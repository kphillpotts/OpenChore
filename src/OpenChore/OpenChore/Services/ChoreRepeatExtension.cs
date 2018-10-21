using System;
using Dates.Recurring;
using OpenChore.Models;

namespace OpenChore.Services
{
    public static class ChoreRepeatExtension
    {

        public static bool RecursOnDate(this ChoreDefinition chore, DateTime checkDate)
        {
            // if chore not active then should not be valid
            if (chore.Active == false)
                return false;

            // if non recurring task, is it for today
            if (chore.IsRepeating == false)
            {
                return chore.StartDateTime.Date == checkDate.Date;
            }

            if (chore.RepeatingUnit == RepeatingUnitType.Day)
            {
                var daily = Recurs.Starting(chore.StartDateTime.Date)
                                  .Every(chore.RepeatingEvery)
                                  .Days()
                                  //.Ending(checkDate.Date.AddDays(1))
                                  
                                  .Build();
                var next = daily.Next(checkDate.AddDays(-1));
                if (next.HasValue)
                {
                    return next.Value.Date == checkDate.Date;
                }
                else
                {
                    return false;
                }
            }

            if (chore.RepeatingUnit == RepeatingUnitType.Week)
            {
                var weekly = Recurs.Starting(chore.StartDateTime.Date)
                                   .Every(chore.RepeatingEvery)
                                   .Weeks()
                                   .OnDays((Day)chore.RepeatingDaysOfWeek)
                                   .Build();
                var next = weekly.Next(checkDate.AddDays(-1));
                if (next.HasValue)
                {
                    return next.Value.Date == checkDate.Date;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }
    }
}
