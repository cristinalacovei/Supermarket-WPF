using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF_LoginForm.Models;
using WPF_LoginForm.Repositories;

namespace WPF_LoginForm.ViewModels
{
    public class BonuriCasaViewModel: ViewModelBase
    {
        private readonly BonuriCasaRepository _bonuriCasaRepository;
        private ObservableCollection<UserModel> _users;
        private UserModel _selectedUser;
        private ObservableCollection<int> _months;
        private int _selectedMonth;
        private ObservableCollection<DailyTotalModel> _dailyTotals;

        public ObservableCollection<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

        public ObservableCollection<int> Months
        {
            get { return _months; }
            set
            {
                _months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        public int SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged(nameof(SelectedMonth));
                LoadDailyTotals();
            }
        }

        public ObservableCollection<DailyTotalModel> DailyTotals
        {
            get { return _dailyTotals; }
            set
            {
                _dailyTotals = value;
                OnPropertyChanged(nameof(DailyTotals));
            }
        }

        public ICommand LoadDataCommand { get; }

        public BonuriCasaViewModel()
        {
            _bonuriCasaRepository = new BonuriCasaRepository();
            LoadUsers();
            LoadMonths();
            LoadDataCommand = new RelayCommand(LoadDailyTotals);
        }

        private void LoadUsers()
        {
            var userRepository = new UserRepository();
            Users = new ObservableCollection<UserModel>(userRepository.GetAllCashiers());
        }

        private void LoadMonths()
        {
            Months = new ObservableCollection<int>(Enumerable.Range(1, 12));
            SelectedMonth = DateTime.Today.Month; // Select current month by default
        }

        private bool _isOkButtonPressed;

        public bool IsOkButtonPressed
        {
            get { return _isOkButtonPressed; }
            set
            {
                _isOkButtonPressed = value;
                OnPropertyChanged(nameof(IsOkButtonPressed));
            }
        }

        private ICommand _okButtonCommand;
        public ICommand OkButtonCommand
        {
            get
            {
                if (_okButtonCommand == null)
                {
                    _okButtonCommand = new RelayCommand(OkButtonCommandExecute);
                }
                return _okButtonCommand;
            }
        }

        private void LoadDailyTotals()
        {
            if (IsOkButtonPressed)
            {
                if (SelectedUser != null && SelectedMonth != 0)
                {
                    DailyTotals = new ObservableCollection<DailyTotalModel>(_bonuriCasaRepository.GetDailyTotalsByUserAndMonth(int.Parse(SelectedUser.Id), SelectedMonth, DateTime.Today.Year));
                }
                else
                {
                    MessageBox.Show("Selectați un utilizator și o lună.", "Eroare", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void OkButtonCommandExecute()
        {
            IsOkButtonPressed = true;
            LoadDailyTotals();
        }
    }
}
