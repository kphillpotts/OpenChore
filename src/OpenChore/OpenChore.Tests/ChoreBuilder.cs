using System;
using System.Collections.Generic;
using OpenChore.Models;
using OpenChore.Services;

namespace OpenChore.Tests
{
    public class ChoreBuilder
    {
        private string name = "A Chore";
        private string description = "Chore Description";
        private bool active = true;
        private List<User> assignedTo = new List<User>();
        private int points = 25;
        private bool isRepeating = false;
        private int repeatEvery = 0;
        private RepeatingUnitType repeatingUnit = RepeatingUnitType.None;
        private DateTime startDate = DateTime.Now;
        private int repeatingDaysOfWeek = DaysOfWeekUtils.EveryDayCode;

        public ChoreBuilder()
        {
        }

        public ChoreDefinition Build()
        {
            var newChore = new ChoreDefinition();
            newChore.Name = name;
            newChore.Description = description;
            newChore.Active = active;
            newChore.AllocatedTo = assignedTo;
            newChore.Points = points;
            newChore.IsRepeating = isRepeating;
            newChore.RepeatingEvery = repeatEvery;
            newChore.RepeatingUnit = repeatingUnit;
            newChore.StartDateTime = startDate;
            newChore.RepeatingDaysOfWeek = repeatingDaysOfWeek;
            return newChore;
        }

        public ChoreBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public ChoreBuilder WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public ChoreBuilder WithActive(bool active)
        {
            this.active = active;
            return this;
        }

        public ChoreBuilder WithAllocations(List<User> users)
        {
            this.assignedTo = users;
            return this;
        }

        public ChoreBuilder WithPoints(int points)
        {
            this.points = points;
            return this;
        }

        public ChoreBuilder WithDaysOfWeek(int daysOfWeek)
        {
            this.repeatingDaysOfWeek = daysOfWeek;
            return this;
        }

        public ChoreBuilder WithRepeating(int every, RepeatingUnitType repeatUnit)
        {
            this.isRepeating = true;
            this.repeatEvery = every;
            this.repeatingUnit = repeatUnit;
            this.repeatingDaysOfWeek = DaysOfWeekUtils.EveryDayCode;
            return this;
        }


        public ChoreBuilder WithStartDate(DateTime startDate)
        {
            this.startDate = startDate;
            return this;
        }

        public ChoreBuilder OnceOff(DateTime startDate)
        {
            this.isRepeating = false;
            this.repeatEvery = 0;
            this.repeatingUnit = RepeatingUnitType.None;
            this.startDate = startDate;
            return this;
        }
    }
}
