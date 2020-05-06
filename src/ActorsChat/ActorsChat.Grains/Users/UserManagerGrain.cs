using ActorsChat.Contracts.Chats;
using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Types;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Users
{
    [StatelessWorker]
    [Reentrant]
    public class UserManagerGrain : Grain, IUserManager
    {
        public async Task<string> AuthenticateUser(Credentials credentials)
        {
            var authentication = GrainFactory.GetGrain<IUserAuthentication>(credentials.Nickname);
            if(await authentication.Authenticate(credentials.Password))
            {
                return await GrainFactory.GetGrain<ITokenManager>(this.GetPrimaryKeyLong()).Create();
            }

            return string.Empty;
        }

        public Task EndAuthentication(string token)
        {
            return GrainFactory.GetGrain<ITokenManager>(this.GetPrimaryKeyLong()).Cancel(token);
        }

        public async Task<string> CreateUser(Credentials credentials)
        {
            var userGrain = GrainFactory.GetGrain<IUserProfile>(credentials.Nickname);
            if(await userGrain.IsAvailable())
            {
                var authentication = GrainFactory.GetGrain<IUserAuthentication>(credentials.Nickname);
                if( await authentication.ChangePassword("", credentials.Password))
                {
                    return await AuthenticateUser(credentials);
                }
            }

            return "";
        }

        public Task<bool> ChangePassword(UserCredentialsChange credentials)
        {
            return GrainFactory.GetGrain<IUserAuthentication>(credentials.Nickname).ChangePassword(credentials.OldPassword, credentials.NewPassword);
        }

        public async Task<UserProfile> GetUser(string nickname)
        {
            var userGrain = GrainFactory.GetGrain<IUserProfile>(nickname);
            if(await userGrain.IsAvailable())
            {
                return await userGrain.Get();
            }

            return null;
        }

        public Task<IEnumerable<Guid>> GetUserChats(string nickname)
        {
            return GrainFactory.GetGrain<IUserProfile>(nickname).GetChats();
        }
    }
}
