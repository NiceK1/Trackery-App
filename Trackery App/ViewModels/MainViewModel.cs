using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;
using System.Windows.Input;
using Trackery_App.Models;
using Trackery_App.Infrastructure.Repositories;
using System.Threading;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Trackery_App.ViewModels
{
    internal class MainViewModel : ObservableObject
    {


        public ICommand HomeViewCommand { get; set; }
        public ICommand DiscoveryViewCommand { get; set; }
        public ICommand EmployeesViewCommand { get; set; }
        public ICommand UserSettingsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }
        public EmployeesViewModel EmployeesVM { get; set; }
        public UserSettingsViewModel UserSettingsVM { get; set; }
        private IUserRepository _userRepository;
        public ObservableCollection<UserModel> Employees
        {
            get;
            set;
        }
        public ObservableCollection<string> RoleOptions { get; }
  = new ObservableCollection<string> { "admin", "boss", "employee" };
        public ObservableCollection<string> StatusOptions { get; }
  = new ObservableCollection<string> { "Present", "Absent" };

        private object _currentView;
        private string _currentUsername;
        private string _role;
        private UserModel _currentUser;
        public UserModel CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; OnPropertyChanged(); }
        }
        private readonly string _picturePath;
        public string PicturePath => _picturePath;
        public string CurrentUsername
        {
            get { return _currentUsername; }
            set { _currentUsername = value; OnPropertyChanged(); }
        }
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            LoadEmployeesData();
            _picturePath = $"/Images/{_role}-avatar.png";
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            EmployeesVM = new EmployeesViewModel();
            UserSettingsVM = new UserSettingsViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });
            EmployeesViewCommand = new RelayCommand(o =>
            {
                CurrentView = EmployeesVM;
            });
            UserSettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = UserSettingsVM;
            });

        }
        private void LoadCurrentUserAccount()
        {
            if (_currentUser != null)
            {
                CurrentUsername = _currentUser.Username;
                _role = _currentUser.Role;
            }
            else
            {
                MessageBox.Show("User not found.");
                Application.Current.Shutdown();
            }

        }
        public void LoadEmployeesData()
        {
            var allUsers = _userRepository.GetAllUsers();
            Employees = new ObservableCollection<UserModel>(allUsers);
            foreach (var user in allUsers)
            {
                user.PropertyChanged += OnUserModelChanged;
                if (user.Username == Thread.CurrentPrincipal.Identity.Name)
                {
                    _currentUser = user;
                }
            }
            LoadCurrentUserAccount();
        }

        private void OnUserModelChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is UserModel user)
            {
                if (e.PropertyName == nameof(UserModel.Email) || e.PropertyName == nameof(UserModel.Status) || e.PropertyName == nameof(UserModel.FirstName) || e.PropertyName == nameof(UserModel.LastName) || e.PropertyName == nameof(UserModel.Role))
                {
                    _userRepository.UpdateUser(user);
                }
            }
        }
    }
}
