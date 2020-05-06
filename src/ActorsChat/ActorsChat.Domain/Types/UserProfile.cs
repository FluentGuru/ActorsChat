using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public class UserProfile
    {
        public UserProfile()
        {
            Chats = Enumerable.Empty<Guid>();
        }

        public string Nickname { get; set; }
        public UserStatuses Status { get; set; }
        public DateTime CreateDate { get; set; }
        public IEnumerable<Guid> Chats { get; set; }
    }
}
