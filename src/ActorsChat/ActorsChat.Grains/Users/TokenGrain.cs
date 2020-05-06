using ActorsChat.Contracts.Users;
using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.Grains.Users
{

    public class TokenGrain : Grain, IToken
    {
        private bool isValid;

        public Task Cancel()
        {
            isValid = false;
            DeactivateOnIdle();
            return Task.CompletedTask;
        }

        public Task<bool> IsValid()
        {
            return Task.FromResult(isValid);
        }

        public Task Refresh(TimeSpan liveTime)
        {
            isValid = true;
            DelayDeactivation(liveTime);
            return Task.CompletedTask;
        }
    }
}
