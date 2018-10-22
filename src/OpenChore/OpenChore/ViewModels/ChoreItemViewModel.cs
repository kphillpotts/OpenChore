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
        private string _choreStatus;



        public string ChoreName { get => _choreModel.Name; set => _choreModel.Name = value; }
        public string ChoreDescription { get => _choreModel.Description; set => _choreModel.Description = value; }
        public int ChorePoints { get => _choreModel.Points; set => _choreModel.Points = value; }
        public string ChoreStatus { get => _choreStatus; set => SetProperty(ref _choreStatus, value); }

        public ChoreItemViewModel(string userName, ChoreDefinition chore, ChoreAward award)
        {
            this._awardModel = award;
            this._choreModel = chore;
            _userName = userName;

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
            UpdateAward(award);
            ViewModelLocator.MainViewModel.SelectedUser.CalculatePointsCommand.Execute(null);
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
