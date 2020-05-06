using ActorsChat.Domain.Services;
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
    public class UserAuthenticationGrainTests : TestKitBase
    {
        [Test]
        public async Task ShouldCreateNewPasswordWhenSendingNullOldPassword()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");

            await authentication.ChangePassword(null, "Pass@123");

            Assert.AreEqual("Pass@123SALT", Silo.StorageManager.GetStorage<UserAuthenticationState>().State.PasswordHash);
        }

        [Test]
        public async Task ShouldChangePasswordWhenSendingCorrectOldPassword()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");

            await authentication.ChangePassword(null, "Pass@123");

            Assert.IsTrue(await authentication.ChangePassword("Pass@123", "Pass@456"));
            Assert.AreEqual("Pass@456SALT", Silo.StorageManager.GetStorage<UserAuthenticationState>().State.PasswordHash);
        }

        [Test]
        public async Task ShouldFailPasswordChangeWhenSendingIncorrectOldPassword()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");

            await authentication.ChangePassword(null, "Pass@123");

            Assert.IsFalse(await authentication.ChangePassword("Pass@12", "Pass@456"));
            Assert.AreEqual("Pass@123SALT", Silo.StorageManager.GetStorage<UserAuthenticationState>().State.PasswordHash);
        }

        [Test]
        public async Task ShouldFailAuthenticationWhenSendingWrongPassword()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");
            await authentication.ChangePassword(null, "Pass@123");

            Assert.IsFalse(await authentication.Authenticate("Pass2"));
        }

        [Test]
        public async Task ShouldAuthenticateUserWhenSendingCorrectPassword()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");
            await authentication.ChangePassword(null, "Pass@123");

            Assert.IsTrue(await authentication.Authenticate("Pass@123"));
        }

        [Test]
        public async Task ShouldReshashPasswordOnSuccesfulAuthentication()
        {
            var hasher = Substitute.For<IHasher>();
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT");
            hasher.Generate(Arg.Any<string>(), Arg.Any<string>()).Returns(c => c.ArgAt<string>(0) + c.ArgAt<string>(1));
            Silo.AddService(hasher);
            var authentication = await Silo.CreateGrainAsync<UserAuthenticationGrain>("johndoe");
            await authentication.ChangePassword(null, "Pass@123");
            hasher.GetSalt(Arg.Any<int>()).Returns("SALT2");

            Assert.IsTrue(await authentication.Authenticate("Pass@123"));
            Assert.AreEqual("Pass@123SALT2", Silo.StorageManager.GetStorage<UserAuthenticationState>().State.PasswordHash);
        }
    }
}
