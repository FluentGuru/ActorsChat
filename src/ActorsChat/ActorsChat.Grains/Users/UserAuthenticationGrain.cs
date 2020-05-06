using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Services;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Users
{
    public class UserAuthenticationState
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }

    public class UserAuthenticationGrain : Grain<UserAuthenticationState>, IUserAuthentication
    {
        private readonly IHasher hasher;

        public UserAuthenticationGrain(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public async Task<bool> Authenticate(string password)
        {
            if(IsValidAuthentication())
            {
                return await ChangePassword(password, password); 
            }

            return false;
        }

        private bool IsValidAuthentication() => !string.IsNullOrEmpty(State?.Salt); 

        public async Task<bool> ChangePassword(string oldPassword, string newPassword)
        {
            var valid = !IsValidAuthentication() || ValidatePassword(oldPassword);
            if(valid)
            {
                await SetNewPassword(newPassword);
            }

            return valid;
        }

        private bool ValidatePassword(string password)
        {
            return State.PasswordHash == hasher.Generate(password, State.Salt);
        }

        private async Task SetNewPassword(string password)
        {
            State.Salt = hasher.GetSalt(8);
            State.PasswordHash = hasher.Generate(password, State.Salt);
            await WriteStateAsync();
        }
    }
}
