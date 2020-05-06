using ActorsChat.Contracts.Chats;
using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Chats
{
    public class ChatListGrain : Grain<List<Chat>>, IChatList
    {
        public async Task Add(Chat chat)
        {
            State.Add(chat);
            await WriteStateAsync();
        }

        public Task<IEnumerable<Chat>> GetAll()
        {
            return Task.FromResult(State.AsEnumerable());
        }

        public async Task Remove(Guid chat)
        {
            State.RemoveAll(c => c.Id == chat);
            await WriteStateAsync();
        }
    }
}
