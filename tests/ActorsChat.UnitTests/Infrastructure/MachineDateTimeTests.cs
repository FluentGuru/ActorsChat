using ActorsChat.Infrastructure.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActorsChat.UnitTests.Infrastructure
{
    public class MachineDateTimeTests
    {
        [Test]
        public void ShouldReturnSystemDate()
        {
            var now = new MachineDateTime().Now;

            Assert.AreEqual(DateTime.Now.Date, now.Date);
        }
    }
}
