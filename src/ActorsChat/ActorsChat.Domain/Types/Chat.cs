using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
