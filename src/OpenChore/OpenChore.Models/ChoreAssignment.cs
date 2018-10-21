using System;
using System.ComponentModel;

namespace OpenChore.Models
{
    public class ChoreAssignment : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChoreDfinitionId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
