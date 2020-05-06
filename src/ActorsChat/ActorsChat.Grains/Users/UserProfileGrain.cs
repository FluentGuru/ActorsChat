using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Services;
using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Users
{
    public class UserProfileGrain : Grain<UserProfile>, IUserProfile
    {
        private readonly IDateTime dateTime;

        public UserProfileGrain(IDateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public Task<bool> IsAvailable() => Task.FromResult(string.IsNullOrEmpty(State.Nickname));

        public Task<UserProfile> Get()
        {
            return Task.FromResult(State ?? new UserProfile());
        }

        public Task<IEnumerable<Guid>> GetChats()
        {
            return Task.FromResult(State.Chats);
        }

        public Task<UserStatuses> GetStatus()
        {
            return Task.FromResult(State.Status);
        }

        public async Task Particpate(Guid chat)
        {
            State.Chats = State.Chats.Concat(new[] { chat });
            await WriteStateAsync();
        }

        public async Task SetStatus(UserStatuses status)
        {
            State.Status = status;
            await WriteStateAsync();
        }

        public async Task Start()
        {
            State = new UserProfile() { Nickname = this.GetPrimaryKeyString(), CreateDate = dateTime.Now, Status = UserStatuses.Online };
            await WriteStateAsync();
        }
    }
}
