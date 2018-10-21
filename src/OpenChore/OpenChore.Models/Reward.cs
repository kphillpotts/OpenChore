using System;
using System.ComponentModel;

namespace OpenChore.Models
{
    public class Reward : INotifyPropertyChanged
    {
        public Reward()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int RedeemValue { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
