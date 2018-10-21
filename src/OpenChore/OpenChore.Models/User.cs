using System;
using System.ComponentModel;
using MvvmHelpers;
namespace OpenChore.Models
{
    public class User : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        //public int Points { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
