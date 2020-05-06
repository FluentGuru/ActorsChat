using ActorsChat.Domain.Types;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Chats
{
    public interface IChatParticipations : IGrainWithGuidKey
    {
        Task SetParticipant(string nickname, UserStatuses status);
        Task RemoveParticipant(string nickname);
        Task<IEnumerable<ChatParticipant>> GetParticipants();
    }
}
