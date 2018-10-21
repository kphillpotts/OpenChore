using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using OpenChore.Models;
using Xamarin.Forms;

namespace OpenChore.ViewModels
{
    public class RewardsViewModel : ViewModelBase
    {
        public User User { get; set; }
        public ObservableRangeCollection<RewardDisplay> Rewards { get; set; } = new ObservableRangeCollection<RewardDisplay>();

        public ICommand LoadRewardCommand { get; private set; }

        public RewardsViewModel()
        {
            this.User = ActiveUser;
            LoadRewardCommand = new Command(async () => await ExecuteLoadRewards());
        }

        public async Task ExecuteLoadRewards()
        {
            var rewards = await StoreManager.RewardStore.GetItemsAsync();
            foreach (var reward in rewards)
            {
                Rewards.Add(new RewardDisplay(reward, User));
            }
        }

    }

    public class RewardDisplay : ObservableObject
    {
        private string _name;
        private string _remainingPoints;
        private int _redeemPoints;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string RemainingPoints
        {
            get { return _remainingPoints; }
            set { SetProperty(ref _remainingPoints, value); }
        }

        public int RedeemPoints
        {
            get { return _redeemPoints; }
            set { SetProperty(ref _redeemPoints, value); }
        }

        public RewardDisplay(Reward reward, User user)
        {
            Name = reward.Name;
            RedeemPoints = reward.RedeemValue;
            //RemainingPoints = user.Points > RedeemPoints ? "Unlocked!" : $"{RedeemPoints - user.Points} points remaining";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
