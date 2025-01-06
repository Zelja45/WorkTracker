using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WorkTracker.Components;
using WorkTracker.Components.ViewModels;
using WorkTracker.Model;
using WorkTracker.Services;
using WorkTracker.Utils;
using WorkTracker.ViewModel.Core;

namespace WorkTracker.ViewModel
{
    public class AddSectorViewModel:BaseViewModel
    {
        private string _name="";
        private string _description="";
        private string _dailyHoursNorm="";
        private string _weeklyHoursNorm="";
        private string _overtimeHourlyRate="";
        private string _hourlyRate="";
        private bool _isAddingable = false;
        private UserService _userService;
        private SectorService _sectorService;
        private List<User> _selectedManagers=new List<User>();
        public List<ManagerWorkerAddingSectorCardViewModel> Cards { get; set; }=new List<ManagerWorkerAddingSectorCardViewModel>();

        public string Name { get { return _name; } set { _name = value; CheckAddingable(); OnPropertyChanged(); } }
        public string Description { get { return _description; } set { _description = value; CheckAddingable(); OnPropertyChanged(); } }
        public string DailyHoursNorm { get { return _dailyHoursNorm; } set { _dailyHoursNorm = value; CheckAddingable(); OnPropertyChanged(); } }
        public string WeeklyHoursNorm { get { return _weeklyHoursNorm; } set { _weeklyHoursNorm = value; CheckAddingable(); OnPropertyChanged(); } }
        public string OvertimeHourlyRate { get { return _overtimeHourlyRate; } set { _overtimeHourlyRate = value; CheckAddingable(); OnPropertyChanged(); } }
        public string HourlyRate { get { return _hourlyRate; } set { _hourlyRate = value; CheckAddingable(); OnPropertyChanged(); } }

        public bool IsAddingable { get { return _isAddingable; } set { _isAddingable = value; OnPropertyChanged(); } }

        public RelayCommand AddSectorCommand { get; set; }
        
        public AddSectorViewModel(SectorService sectorService,UserService userService)
        {
            _userService = userService;
            _sectorService = sectorService;
            AddSectorCommand = new RelayCommand(async o => {  await AddNewSector(); }, o => true);
        }
        private async System.Threading.Tasks.Task AddNewSector()
        {
            ResourceDictionary resources = Application.Current.Resources;
            try
            {
               
                Sector sector = new Sector
                {
                    HourlyRate = decimal.Parse(_hourlyRate.Trim(), CultureInfo.InvariantCulture),
                    WeeklyHoursNorm = int.Parse(_weeklyHoursNorm.Trim()),
                    DailyHoursNorm = int.Parse(_dailyHoursNorm.Trim()),
                    Description = _description,
                    Name = _name,
                    OvertimeHourlyRate = decimal.Parse(_overtimeHourlyRate.Trim(), CultureInfo.InvariantCulture),
                };
                try
                {
                    await _sectorService.AddNewSector(sector,_selectedManagers);
                    new CustomDialog(false, true, (string)resources["NewSectorIsCreatedTitle"], (string)resources["NewSectorIsCreatedDescription"]).Show();
                    Reset();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    new CustomDialog(false, false, (string)resources["ErorrOnAddingTitle"], (string)resources["TryAgainLater"]).Show();
                }
            }
            catch (Exception ex)
            {

                new CustomDialog(false, false, (string)resources["ErorrOnAddingTitle"], (string)resources["InvalidEntryDescription"]).Show();
            }


        }
        public override async System.Threading.Tasks.Task Initialize()
        {
            List<User> managers=await _userService.GetUsersByType(Constants.ManagerKeyWord);
            foreach (User user in managers)
            {
                ManagerWorkerAddingSectorCardViewModel vm=new ManagerWorkerAddingSectorCardViewModel(user);
                vm.Command = new RelayCommand(o => { if (_selectedManagers.Contains(user)) { _selectedManagers.Remove(user); } else _selectedManagers.Add(user); CheckAddingable(); }, o => true);
                Cards.Add(vm);
            }
        }
        private void CheckAddingable()
        {
            if (_name.Length != 0 && _description.Length != 0 && _dailyHoursNorm.Length != 0 && _weeklyHoursNorm.Length != 0 && _overtimeHourlyRate.Length != 0 && _hourlyRate.Length != 0 && _selectedManagers.Count != 0)
            {
                IsAddingable = true;
            }
            else
            {
                IsAddingable = false;
            }
        }
        public override void Dispose()
        {
            Reset();
            Cards.Clear();
        }
        private void Reset()
        {
            Name = "";
            Description = "";
            DailyHoursNorm = "";
            WeeklyHoursNorm = "";
            OvertimeHourlyRate = "";
            HourlyRate = "";
            IsAddingable = false;
            _selectedManagers.Clear();
            foreach (var card in Cards) 
            {
                card.IsChecked= false;
            }

        }



    }
}
