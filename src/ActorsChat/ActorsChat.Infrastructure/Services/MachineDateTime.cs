using ActorsChat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.Infrastructure.Services
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
