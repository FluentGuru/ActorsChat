using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Chats
{
    public interface IChats : IGrainWithIntegerKey
    {
        Task<Chat> Create(string creator, string name);
        Task JoinChat(Guid chat, string nickname);
        Task LeaveChat(Guid chat, string nickname);
        Task<IEnumerable<Chat>> GetChats();
        Task<IEnumerable<ChatParticipant>> GetParticipants(Guid chat);
        Task SendMessage(Guid chat, string nickname, string message);
    }
}
