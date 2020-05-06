using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public class ChatMessage
    {
        public string Sender { get; set; }
        public Guid ChatId { get; set; }
        public ChatMessageTypes Type { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
    }
}
