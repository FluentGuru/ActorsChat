using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Users
{
    public interface IToken : IGrainWithStringKey
    {
        Task Refresh(TimeSpan liveTime);
        Task Cancel();
        Task<bool> IsValid();
    }
}
