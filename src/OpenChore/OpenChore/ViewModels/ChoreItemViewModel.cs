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

        private ChoreDefinition _choreModel { get; set; }
        private ChoreAward _awardModel { get; set; }
        private string _userName;

        //private string _choreName;
        //private string _choreDescription;
        //private int _chorePoints;

        //public string ChoreName { get => _choreName; set => SetProperty(ref _choreName, value); }
        //public string ChoreDescription { get => _choreDescription; set => SetProperty(ref _choreDescription, value); }
        //public int ChorePoints { get => _chorePoints; set => SetProperty(ref _chorePoints, value); }

        public string ChoreName { get => _choreModel.Name; set => _choreModel.Name = value; }
        public string ChoreDescription { get => _choreModel.Description; set => _choreModel.Description = value; }
        public int ChorePoints { get => _choreModel.Points; set => _choreModel.Points = value; }
        //public string UserName { get => _userModel.Name; set => _userModel.Name = value; }
        //public int ChorePoints { get => _choreModel.Points; set => _choreModel.Points = value; }
        //public string ChoreImage { get => _choreModel.ImageUrl; set => _choreModel.ImageUrl = value; }
        //public ChoreDefinition ChoreDefinition { get => _choreModel; set => _choreModel = value; }
        private string _choreStatus;
        public string ChoreStatus { get => _choreStatus; set => SetProperty(ref _choreStatus, value); }



        //public ChoreItemViewModel(UserViewModel user, ChoreDefinition chore, ChoreAward award)
        //{

        //}

        public ChoreItemViewModel(string userName, ChoreDefinition chore, ChoreAward award)
        {
            //this._userModel = user;
            this._awardModel = award;
            this._choreModel = chore;
            //User = user;
            _userName = userName;
            //ChoreDescription = chore.Description;
            //ChoreImage = chore.ImageUrl;
            //ChoreName = chore.Name;

            //ChoreName = chore.Name;
            //ChoreDescription = chore.Description;
            //ChorePoints = chore.Points;



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

            // add a new award
            ChoreAward award = new ChoreAward();
            award.ChoreDefinitionId = _choreModel.Id;
            award.Points = _choreModel.Points;
            award.UserId = _userName;
            award.ChoreDate = DateTime.Now;
            _awardModel = award;
            await StoreManager.AwardStore.InsertAsync(award);

            // update chorepoints
            //ChorePoints = _choreModel.Points;
            UpdateAward(award);
            ViewModelLocator.MainViewModel.SelectedUser.CalculatePointsCommand.Execute(null);
            //_userModel.Points = await StoreManager.UserStore.GetPoints(_userName);
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
            ViewModelLocator.MainViewModel.SelectedUser.CalculatePointsCommand.Execute(null);
        }


        public void UpdateAward(ChoreAward award)
        {
            ChoreStatus = award != null ? ChoreDescription + " - Done: " + award.Points : ChoreDescription;
        }


    }

}
