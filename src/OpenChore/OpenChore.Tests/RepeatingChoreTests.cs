using System;
using NUnit.Framework;
using OpenChore.Services;
using Should;
namespace OpenChore.Tests
{
    [TestFixture]
    public class RepeatingChoreTests
    {
        [Test()]
        public void DailyRepeatChore_ShouldBeActive()
        {
            var chore = new ChoreBuilder().WithRepeating(1, Models.RepeatingUnitType.Day).Build();
            chore.RecursOnDate(DateTime.Now).ShouldBeTrue();
        }

        [Test()]
        public void DailyRepeatChore_FromYesterday_ShouldBeActiveToday()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Day)
                .WithStartDate(DateTime.Now.AddDays(-1))
                .Build();
            chore.RecursOnDate(DateTime.Now).ShouldBeTrue();
        }

        [Test()]
        public void DailyRepeatChore_StartingTomorrow_ShouldNotBeActiveToday()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Day)
                .WithStartDate(DateTime.Now.AddDays(1))
                .Build();
            chore.RecursOnDate(DateTime.Now).ShouldBeFalse();
        }

        [Test()]
        public void DailyRepeatChore_StartingTomorrow_ShouldNotBeActiveTomorrow()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Day)
                .WithStartDate(DateTime.Now.AddDays(1))
                .Build();
            chore.RecursOnDate(DateTime.Now.AddDays(1)).ShouldBeTrue();
        }

        [Test()]
        public void DailyRepeatChore_StartingToday_ShouldStillBeActiveNextWeek()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Day)
                .WithStartDate(DateTime.Now)
                .Build();
            chore.RecursOnDate(DateTime.Now.AddDays(7)).ShouldBeTrue();
        }

        [Test()]
        public void RepeatChore_EverySecondDay_ShouldBeActiveTodayButNotTomorrow()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(2, Models.RepeatingUnitType.Day)
                .WithStartDate(DateTime.Now)
                .Build();

            // should be active today
            chore.RecursOnDate(DateTime.Now).ShouldBeTrue();
            // should not be active tomorrow
            chore.RecursOnDate(DateTime.Now.AddDays(1)).ShouldBeFalse();
            // should be active again the day after
            chore.RecursOnDate(DateTime.Now.AddDays(2)).ShouldBeTrue();
        }

        [Test()]
        public void RepeatChore_EverySaturday_ShouldBeActiveNextOnSaturday()
        {
            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Week)
                .WithDaysOfWeek(DaysOfWeekUtils.valueOf(DayOfWeek.Saturday))
                .Build();

            DateTime checkDate = DateTime.Now.Date;
            for (int i = 0; i < 7; i++)
            {
                if (checkDate.DayOfWeek == DayOfWeek.Saturday)
                    chore.RecursOnDate(checkDate).ShouldBeTrue();
                else
                    chore.RecursOnDate(checkDate).ShouldBeFalse();

                checkDate = checkDate.AddDays(1);
            }
        }

        [Test()]
        public void RepeatChore_EveryMonWedFri_ShouldOccurOnMonWedFri()
        {
            int daysOfWeek = DaysOfWeekUtils.valueOf( new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday });

            var chore = new ChoreBuilder()
                .WithRepeating(1, Models.RepeatingUnitType.Week)
                .WithDaysOfWeek(daysOfWeek)
                .Build();

            // spin through a week and check we have an occurance on the right days
            DateTime checkDate = DateTime.Now.Date;
            for (int i = 0; i < 7; i++)
            {
                if ((checkDate.DayOfWeek == DayOfWeek.Monday) ||
                    (checkDate.DayOfWeek == DayOfWeek.Wednesday) ||
                    (checkDate.DayOfWeek == DayOfWeek.Friday))
                    chore.RecursOnDate(checkDate).ShouldBeTrue();
                else
                    chore.RecursOnDate(checkDate).ShouldBeFalse();

                checkDate = checkDate.AddDays(1);
            }
        }
    }
}
