using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Services;
using Orleans;
using Orleans.Concurrency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Users
{
    [StatelessWorker]
    [Reentrant]
    public class TokenManagerGrain : Grain, ITokenManager
    {
        private readonly IHasher hasher;
        private readonly TimeSpan liveTime = TimeSpan.FromDays(1);

        public TokenManagerGrain(IHasher hasher)
        {
            this.hasher = hasher;
        }

        public Task Cancel(string token)
        {
            return GrainFactory.GetGrain<IToken>(token).Cancel();
        }

        public async Task<string> Create()
        {
            var token = hasher.Generate(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await GrainFactory.GetGrain<IToken>(token).Refresh(liveTime);
            return token;
        }
    }
}
