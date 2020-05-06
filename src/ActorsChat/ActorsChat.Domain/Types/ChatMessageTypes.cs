using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Types
{
    public enum ChatMessageTypes
    {
        Message,
        Created,
        UserJoined,
        UserLeft
    }
}
