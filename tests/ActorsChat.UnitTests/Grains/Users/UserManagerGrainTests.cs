using ActorsChat.Contracts.Users;
using ActorsChat.Domain.Types;
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
    public class UserManagerGrainTests : TestKitBase
    {
        [Test]
        public async Task ShouldAuthenticateUser()
        {
            var token = Silo.AddProbe<ITokenManager>(0);
            var authentication = Silo.AddProbe<IUserAuthentication>("johndoe");
            token.Setup(s => s.Create()).Returns(Task.FromResult("TOKEN"));
            authentication.Setup(a => a.Authenticate(It.IsAny<string>())).Returns(Task.FromResult(true));
            var manager = await Silo.CreateGrainAsync<UserManagerGrain>(0);

            var t = await manager.AuthenticateUser(new Credentials() { Nickname = "johndoe", Password = "Pass@123" });

            Assert.AreEqual("TOKEN", t);
        }
    }
}
