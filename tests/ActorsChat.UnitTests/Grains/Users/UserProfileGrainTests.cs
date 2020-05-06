using ActorsChat.Domain.Services;
using ActorsChat.Domain.Types;
using ActorsChat.Grains.Users;
using NSubstitute;
using NUnit.Framework;
using Orleans.TestKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ActorsChat.UnitTests.Grains.Users
{
    public class UserProfileGrainTests : TestKitBase
    {
        [Test]
        public async Task ShouldReserveProfileGrainForUserOnStart()
        {
            var date = new DateTime(2020, 5, 6);
            var dateTime = Substitute.For<IDateTime>();
            dateTime.Now.Returns(date);
            Silo.AddService(dateTime);
            var profileGrain = await Silo.CreateGrainAsync<UserProfileGrain>("johndoe");

            await profileGrain.Start();

            var state = Silo.StorageManager.GetStorage<UserProfileState>().State;
            Assert.AreEqual("johndoe", state.Nickname);
            Assert.AreEqual(date, state.CreateDate);
            Assert.AreEqual(UserStatuses.Online, state.Status);
        }

        [Test]
        public async Task ShouldBeAvailableIfNotStarted()
        {
            var date = new DateTime(2020, 5, 6);
            var dateTime = Substitute.For<IDateTime>();
            dateTime.Now.Returns(date);
            Silo.AddService(dateTime);
            var profileGrain = await Silo.CreateGrainAsync<UserProfileGrain>("johndoe");

            Assert.IsTrue(await profileGrain.IsAvailable());
        }

        [Test]
        public async Task ShouldBeUnavailableIfStarted()
        {
            var date = new DateTime(2020, 5, 6);
            var dateTime = Substitute.For<IDateTime>();
            dateTime.Now.Returns(date);
            Silo.AddService(dateTime);
            var profileGrain = await Silo.CreateGrainAsync<UserProfileGrain>("johndoe");

            await profileGrain.Start();

            Assert.IsFalse(await profileGrain.IsAvailable());
        }
    }
}
