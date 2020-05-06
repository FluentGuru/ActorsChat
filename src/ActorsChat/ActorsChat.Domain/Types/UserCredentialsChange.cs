using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public class UserCredentialsChange
    {
        public string Nickname { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
