using ActorsChat.Grains.Users;
using Moq;
using NUnit.Framework;
using Orleans.TestKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.UnitTests.Grains.Users
{
    public class TokenGrainTests : TestKitBase
    {
        [Test]
        public async Task ShouldMakeTokenValidOnRefresh()
        {
            await CreateAndActivateTokenGrain();
        }

        private async Task<TokenGrain> CreateAndActivateTokenGrain()
        {
            var tokenGrain = await Silo.CreateGrainAsync<TokenGrain>(nameof(ShouldMakeTokenValidOnRefresh));

            await tokenGrain.Refresh(TimeSpan.FromDays(1));

            Assert.IsTrue(await tokenGrain.IsValid());

            return tokenGrain;
        }

        [Test]
        public async Task ShouldExtendTokenLiveTimeOnRefresh()
        {
            var tokenGrain = await Silo.CreateGrainAsync<TokenGrain>(nameof(ShouldMakeTokenValidOnRefresh));
            var liveTime = TimeSpan.FromDays(1);

            await tokenGrain.Refresh(liveTime);

            Silo.VerifyRuntime(r => r.DelayDeactivation(tokenGrain, liveTime), Times.Once);
        }

        [Test]
        public async Task ShouldInvalidateTokenOnCancel()
        {
            var token = await CreateAndActivateTokenGrain();

            await token.Cancel();

            Assert.IsFalse(await token.IsValid());
        }

        [Test]
        public async Task ShouldDeactivateTokenOnCancel()
        {
            var token = await CreateAndActivateTokenGrain();

            await token.Cancel();

            Silo.VerifyRuntime(v => v.DeactivateOnIdle(token), Times.Once);
        }
    }
}
