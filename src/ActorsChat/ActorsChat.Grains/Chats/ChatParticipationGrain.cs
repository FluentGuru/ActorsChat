

using ActorsChat.Contracts.Chats;
using ActorsChat.Domain.Types;
using Orleans;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Chats
{
    public class ChatParticipationGrain : Grain<Dictionary<string, UserStatuses>>, IChatParticipations
    {
        public Task<IEnumerable<ChatParticipant>> GetParticipants()
        {
            return Task.FromResult(State.OrderBy(p => p.Key).Select(p => new ChatParticipant()
            {
                ChatId = this.GetPrimaryKey(),
                Nickname = p.Key,
                Status = p.Value
            }));
        }

        public async Task RemoveParticipant(string nickname)
        {
            if(State.ContainsKey(nickname))
            {
                State.Remove(nickname);
                await WriteStateAsync();
            }
        }

        public async Task SetParticipant(string nickname, UserStatuses status)
        {
            State[nickname] = status;
            await WriteStateAsync();
        }
    }
}
