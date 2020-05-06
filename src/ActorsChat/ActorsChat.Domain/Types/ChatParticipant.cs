using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public class ChatParticipant
    {
        public Guid ChatId { get; set; }
        public string Nickname { get; set; }
        public UserStatuses Status { get; set; }
    }
}
