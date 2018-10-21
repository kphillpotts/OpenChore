using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OpenChore.Models
{
    public enum RepeatingUnitType
    {
        None,
        Day,
        Week,
        Month
    }

    public class ChoreDefinition : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Active { get; set; }
        public DateTime StartDateTime { get; set; }
        public bool IsRepeating { get; set; }
        public int RepeatingEvery { get; set; }
        public RepeatingUnitType RepeatingUnit { get; set; }
        public int Points { get; set; }
        public List<User> AllocatedTo { get; set; }
        public int RepeatingDaysOfWeek { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
