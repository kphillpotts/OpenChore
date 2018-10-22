﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;

namespace Dates.Recurring.Type
{
    public class Weekly : RecurrenceType
    {
        public Day Days { get; set; }
        public DayOfWeek FirstDayOfWeek { get; set; }
        private DayOfWeek LastDayOfWeek
        {
            get
            {
                if (this.FirstDayOfWeek == DayOfWeek.Sunday)
                {
                    return DayOfWeek.Saturday;
                }
                else
                {
                    return this.FirstDayOfWeek - 1;
                }
            }
        }

        public Weekly(int weeks, DateTime starting, DateTime? ending, Day days, DayOfWeek firstDayOfWeek) : base(weeks, starting, ending)
        {
            Days = days;
            FirstDayOfWeek = firstDayOfWeek;
        }

        public override DateTime? Next(DateTime after)
        {
            var next = Starting;

            if (after.Date < Starting.Date)
            {
                after = Starting - 1.Days();
            }

            while (next.Date <= after.Date || !DayOfWeekMatched(next.DayOfWeek))
            {
                if (next.DayOfWeek != LastDayOfWeek)
                {
                    next = next + 1.Days();
                }
                else
                {
                    // Skip ahead by x weeks.
                    next = next + X.Weeks();

                    // Rewind to the first day of the week.
                    int delta = FirstDayOfWeek - next.DayOfWeek;
                    if (delta > 0)
                    {
                        delta -= 7;
                    }
                    next = next + delta.Days();
                }
            }

            if (Ending.HasValue && next.Date > Ending.Value.Date)
            {
                return null;
            }

            return next;
        }

        private bool DayOfWeekMatched(DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday && (Days & Day.SUNDAY) != 0)
                return true;

            if (day == DayOfWeek.Monday && (Days & Day.MONDAY) != 0)
                return true;

            if (day == DayOfWeek.Tuesday && (Days & Day.TUESDAY) != 0)
                return true;

            if (day == DayOfWeek.Wednesday && (Days & Day.WEDNESDAY) != 0)
                return true;

            if (day == DayOfWeek.Thursday && (Days & Day.THURSDAY) != 0)
                return true;

            if (day == DayOfWeek.Friday && (Days & Day.FRIDAY) != 0)
                return true;

            if (day == DayOfWeek.Saturday && (Days & Day.SATURDAY) != 0)
                return true;

            return false;
        }
    }
}
