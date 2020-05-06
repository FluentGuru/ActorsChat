using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Domain.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
