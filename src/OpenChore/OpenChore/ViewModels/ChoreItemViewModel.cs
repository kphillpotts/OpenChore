using System;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenChore.Models;
using Xamarin.Forms;

namespace OpenChore.ViewModels
{
    public class ChoreItemViewModel : ViewModelBase
    {
        public ICommand ChoreDoneCommand { get; set; }
        public ICommand ChoreUndoneCommand { get; set; }

        private User _userModel { get; set; }
        private ChoreDefinition _choreModel { get; set; }
        private ChoreAward _awardModel { get; set; }

        public User User { get; set; }

        public string ChoreName { get => _choreModel.Name; set => _choreModel.Name = value; }
        public string ChoreDescription { get => _choreModel.Description; set => _choreModel.Description = value; }
        public string UserName { get => _userModel.Name; set => _userModel.Name = value; }
        public int ChorePoints { get => _choreModel.Points; set => _choreModel.Points = value; }
        public string ChoreImage { get => _choreModel.ImageUrl; set => _choreModel.ImageUrl = value; }
        public ChoreDefinition ChoreDefinition { get => _choreModel; set => _choreModel = value; }
        private string _choreStatus;
        public string ChoreStatus { get => _choreStatus; set => SetProperty(ref _choreStatus, value); }

        public ChoreItemViewModel(User user, ChoreDefinition chore, ChoreAward award)
        {
            this._userModel = user;
            this._choreModel = chore;
            User = user;
            UserName = user.Name;
            ChoreDescription = chore.Description;
            ChoreImage = chore.ImageUrl;
            ChoreName = chore.Name;

            UpdateAward(award);
            ChoreDoneCommand = new Command(async () => await ChoreDoneExecute());
            ChoreUndoneCommand = new Command(async () => await ChoreUndoneExecute());
        }

        private async Task ChoreDoneExecute()
        {
            // if we already have an award for the chore
            // nothing to do.
            if (_awardModel != null)
                return;

            ChoreAward award = new ChoreAward();
            award.ChoreDefinitionId = _choreModel.Id;
            award.Points = _choreModel.Points;
            award.UserId = _userModel.Name;
            award.ChoreDate = DateTime.Now;
            await StoreManager.AwardStore.InsertAsync(award);
            UpdateAward(award);
            _userModel.Points = await StoreManager.UserStore.GetPoints(_userModel.Name);
        }

        private async Task ChoreUndoneExecute()
        {
            // if we don't have an award that's cool.
            if (_awardModel == null)
                return;
        
            // delete the award
            await StoreManager.AwardStore.RemoveAsync(_awardModel);
            _awardModel = null;
            UpdateAward(null);
        }


        public void UpdateAward(ChoreAward award)
        {
            this._awardModel = award;
            if (award != null)
                ChoreStatus = _choreModel.Description + " - Done: " + award.Points;
            else
                ChoreStatus = _choreModel.Description;
        }


    }

}
