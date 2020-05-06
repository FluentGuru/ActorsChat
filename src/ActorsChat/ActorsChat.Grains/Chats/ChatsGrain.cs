using ActorsChat.Contracts.Chats;
using ActorsChat.Domain.Types;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Chats
{
    [StatelessWorker]
    [Reentrant]
    public class ChatsGrain : Grain, IChats
    {
        public async Task<Chat> Create(string creator, string name)
        {
            var id = Guid.NewGuid();
            var chat = await GrainFactory.GetGrain<IChat>(id).Start(creator, name);
            await GrainFactory.GetGrain<IChatList>(this.GetPrimaryKeyLong()).Add(chat);
            return chat;
        }

        public Task<IEnumerable<Chat>> GetChats()
        {
            return GrainFactory.GetGrain<IChatList>(this.GetPrimaryKeyLong()).GetAll();
        }

        public Task<IEnumerable<ChatParticipant>> GetParticipants(Guid chat)
        {
            return GrainFactory.GetGrain<IChatParticipations>(chat).GetParticipants();
        }

        public Task JoinChat(Guid chat, string nickname)
        {
            return GrainFactory.GetGrain<IChat>(chat).Join(nickname);
        }

        public Task LeaveChat(Guid chat, string nickname)
        {
            return GrainFactory.GetGrain<IChat>(chat).Leave(nickname);
        }

        public Task SendMessage(Guid chat, string nickname, string message)
        {
            return GrainFactory.GetGrain<IChat>(chat).SendMessage(nickname, message);
        }
    }
}
