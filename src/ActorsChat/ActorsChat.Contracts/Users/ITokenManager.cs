using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Contracts.Users
{
    public interface ITokenManager : IGrainWithIntegerKey
    {
        Task<string> Create();
        Task Cancel(string token);
    }
}
