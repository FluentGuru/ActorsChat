using ActorsChat.Domain.Types;
using Orleans;
using Orleans.Streams;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Chats
{
    public interface IChat : IGrainWithGuidKey
    {
        Task<Chat> Start(string creator, string name);
        Task Join(string nickname);
        Task Leave(string nickname);
        Task<IChatParticipations> GetParticipations();
        Task<IChatMessages> GetMessages();
        Task SendMessage(string nickname, string message);
    }
}
