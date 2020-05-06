using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Chats
{
    public interface IChatList : IGrainWithIntegerKey
    {
        Task Add(Chat chat);
        Task Remove(Guid chat);
        Task<IEnumerable<Chat>> GetAll();
    }
}
