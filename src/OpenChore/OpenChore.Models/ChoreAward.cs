using System;
using System.ComponentModel;

namespace OpenChore.Models
{
    public class ChoreAward : INotifyPropertyChanged
    {
        public string UserId { get; set; }
        public int ChoreDefinitionId { get; set; }
        public string Message { get; set; }
        public DateTime ChoreDate { get; set; }
        public bool IsApproved { get; set; }
        public int Points { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
