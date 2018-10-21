using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenChore.Services
{
    // Code borrowed (with modifications)
    // https://www.codeproject.com/Tips/265399/Converting-Days-of-the-Week-to-and-From-a-BitFlag
    public static class DaysOfWeekUtils
    {
        #region Nested Enums
        [Flags]
        private enum DayOfWeek
        {
            Sunday = 0x1,
            Monday = 0x2,
            Tuesday = 0x4,
            Wednesday = 0x8,
            Thursday = 0x10,
            Friday = 0x20,
            Saturday = 0x40
        }
        #endregion

        #region Private Fields
        private static Dictionary<DayOfWeek, System.DayOfWeek> systemDayOfWeek;
        private static Dictionary<System.DayOfWeek, DayOfWeek> bitFlagDayOfWeek;
        #endregion

        #region Constructors
        static DaysOfWeekUtils()
        {
            systemDayOfWeek = new Dictionary<DayOfWeek, System.DayOfWeek>
            {
                [DayOfWeek.Sunday] = System.DayOfWeek.Sunday,
                [DayOfWeek.Monday] = System.DayOfWeek.Monday,
                [DayOfWeek.Tuesday] = System.DayOfWeek.Tuesday,
                [DayOfWeek.Wednesday] = System.DayOfWeek.Wednesday,
                [DayOfWeek.Thursday] = System.DayOfWeek.Thursday,
                [DayOfWeek.Friday] = System.DayOfWeek.Friday,
                [DayOfWeek.Saturday] = System.DayOfWeek.Saturday
            };

            bitFlagDayOfWeek = new Dictionary<System.DayOfWeek, DayOfWeek>
            {
                [System.DayOfWeek.Sunday] = DayOfWeek.Sunday,
                [System.DayOfWeek.Monday] = DayOfWeek.Monday,
                [System.DayOfWeek.Tuesday] = DayOfWeek.Tuesday,
                [System.DayOfWeek.Wednesday] = DayOfWeek.Wednesday,
                [System.DayOfWeek.Thursday] = DayOfWeek.Thursday,
                [System.DayOfWeek.Friday] = DayOfWeek.Friday,
                [System.DayOfWeek.Saturday] = DayOfWeek.Saturday
            };

            MondayToFridayCode = valueOf(System.DayOfWeek.Monday, System.DayOfWeek.Tuesday,
                System.DayOfWeek.Wednesday, System.DayOfWeek.Thursday, System.DayOfWeek.Friday);

            EveryDayCode = valueOf
                (
                    System.DayOfWeek.Sunday,
                    System.DayOfWeek.Monday,
                    System.DayOfWeek.Tuesday,
                    System.DayOfWeek.Wednesday,
                    System.DayOfWeek.Thursday,
                    System.DayOfWeek.Friday,
                    System.DayOfWeek.Saturday
                );
        }
        #endregion

        #region Public Properties
        public static int MondayToFridayCode { get; private set; }
        public static int EveryDayCode { get; private set; }

        public static IEnumerable<System.DayOfWeek> EachDayOfTheWeek
        {
            get
            {
                yield return System.DayOfWeek.Sunday;
                yield return System.DayOfWeek.Monday;
                yield return System.DayOfWeek.Tuesday;
                yield return System.DayOfWeek.Wednesday;
                yield return System.DayOfWeek.Thursday;
                yield return System.DayOfWeek.Friday;
                yield return System.DayOfWeek.Saturday;
            }
        }

        public static IEnumerable<System.DayOfWeek> MondayToFriday
        {
            get
            {
                return daysOfWeek(MondayToFridayCode);
            }
        }

        #endregion

        #region Public Methods
        public static IEnumerable<System.DayOfWeek> daysOfWeek(int val)
        {
            foreach (DayOfWeek bitFlag in systemDayOfWeek.Keys)
                if ((val & (int)bitFlag) == (int)bitFlag)
                    yield return systemDayOfWeek[bitFlag];
        }

        public static int valueOf(params System.DayOfWeek[] daysOfWeek)
        {
            return daysOfWeek.Distinct().Sum(dayOfWeek => (int)bitFlagDayOfWeek[dayOfWeek]);
        }
        #endregion
    }
}
