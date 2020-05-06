using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Chats
{
    public interface IChatMessages : IGrainWithGuidKey
    {
        Task SendMessage(ChatMessage message);
        Task<IEnumerable<ChatMessage>> GetMessages(int index, int count = 50);
    }
}
