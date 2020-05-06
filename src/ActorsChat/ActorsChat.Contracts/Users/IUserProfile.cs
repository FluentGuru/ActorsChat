using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Users
{
    public interface IUserProfile : IGrainWithStringKey
    {
        Task Start();
        Task<UserProfile> Get();
        Task<bool> IsAvailable();
        Task<UserStatuses> GetStatus();
        Task SetStatus(UserStatuses status);
        Task<IEnumerable<Guid>> GetChats();
        Task Particpate(Guid chat);
    }
}
