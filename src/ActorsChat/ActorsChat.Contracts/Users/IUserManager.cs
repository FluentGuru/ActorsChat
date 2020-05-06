using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Users
{
    public interface IUserManager : IGrainWithIntegerKey
    {
        Task<string> CreateUser(Credentials credentials);
        Task<string> AuthenticateUser(Credentials credentials);
        Task<bool> ChangePassword(UserCredentialsChange credentials);
        Task EndAuthentication(string token);
        Task<UserProfile> GetUser(string nickname);
        Task<IEnumerable<Guid>> GetUserChats(string nickname);
    }
}
