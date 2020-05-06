using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Services;
using ActorsChat.Grains.Users;
using Moq;
using NSubstitute;
using NUnit.Framework;
using Orleans.TestKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.UnitTests.Grains.Users
{
    public class TokerManagerGrainTests : TestKitBase
    {
        [Test]
        public async Task ShouldCreateTokenValueWithHasher()
        {
            TokenManagerGrain tokenManager = await CreateTokenManager();

            var token = await tokenManager.Create();

            Assert.AreEqual("TOKEN", token);
        }

        private async Task<TokenManagerGrain> CreateTokenManager()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns("TOKEN");
            Silo.AddService(hasher);
            var tokenManager = await Silo.CreateGrainAsync<TokenManagerGrain>(0);
            return tokenManager;
        }

        [Test]
        public async Task ShouldCreateActiveTokenGrain()
        {
            var tokenManager = await CreateTokenManager();
            var token = Silo.AddProbe<IToken>("TOKEN");

            await tokenManager.Create();

            token.Verify(t => t.Refresh(It.IsAny<TimeSpan>()), Times.Once);
        }

        [Test]
        public async Task ShouldCancelTokenGrain()
        {
            var tokenManager = await CreateTokenManager();
            var token = Silo.AddProbe<IToken>("TOKEN");

            await tokenManager.Cancel("TOKEN");

            token.Verify(t => t.Cancel(), Times.Once);
        }
    }
}
