using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Users
{
    public interface IUserAuthentication : IGrainWithStringKey
    {
        Task<bool> ChangePassword(string oldPassword, string newPassword);
        Task<bool> Authenticate(string password);
    }
}
