using System;
using NUnit.Framework;
using OpenChore.Models;
using OpenChore.Services;
using Should;
namespace OpenChore.Tests
{
    [TestFixture]
    public class OnceOffChoreTests
    {
        [Test()]
        public void OnceOffChore_ForToday_ShouldBeActiveForToday()
        {


            var chore = new ChoreBuilder()
                .OnceOff(DateTime.Today)
                .Build();

            chore.RecursOnDate(DateTime.Now).ShouldBeTrue();
        }

        [Test()]
        public void OnceOffChore_ForTomorrow_ShouldNotBeActiveToday()
        {
            var chore = new ChoreBuilder()
                .OnceOff(DateTime.Today.AddDays(1))
                .Build();

            chore.RecursOnDate(DateTime.Now).ShouldBeFalse();
        }

        [Test()]
        public void OnceOffChore_ForToday_ShouldNotBeActiveTomorrow()
        {
            var chore = new ChoreBuilder()
                .OnceOff(DateTime.Today)
                .Build();

            chore.RecursOnDate(DateTime.Now.AddDays(1)).ShouldBeFalse();
        }

        [Test()]
        public void OnceOffChore_ForYesterday_ShouldNotBeActiveToday()
        {
            var chore = new ChoreBuilder()
                .OnceOff(DateTime.Today.AddDays(-1))
                .Build();

            chore.RecursOnDate(DateTime.Now).ShouldBeFalse();
        }

        [Test()]
        public void OnceOffChore_ForTodayButInactive_ShouldNotBeActiveToday()
        {
            var chore = new ChoreBuilder()
                .OnceOff(DateTime.Today)
                .WithActive(false)
                .Build();

            chore.RecursOnDate(DateTime.Now).ShouldBeFalse();
        }
    }
}
