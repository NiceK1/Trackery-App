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

namespace Trackery_App.ViewModels
{
    internal class MainViewModel : ObservableObject
    {


        public ICommand HomeViewCommand { get; set; }
        public ICommand DiscoveryViewCommand { get; set; }
        public ICommand EmployeesViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }
        public EmployeesViewModel EmployeesVM { get; set; }
        private IUserRepository _userRepository;
        public ObservableCollection<UserModel> Employees { get; private set; }

        private object _currentView;
        private string _currentUsername;
        private string _role;
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
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _userRepository = new UserRepository();
            LoadCurrentUserAccount();
            LoadEmployeesData();
            _picturePath = $"/Images/{_role}-avatar.png";
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            EmployeesVM = new EmployeesViewModel();
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

        }
        private void LoadCurrentUserAccount()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var user = _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                CurrentUsername = user.Username;
                _role = user.Role;
            }
            else
            {
                MessageBox.Show("User not found.");
                Application.Current.Shutdown();
            }

        }
        public void LoadEmployeesData() {
            Employees = new ObservableCollection<UserModel>(_userRepository.GetAllUsers());
        }
    }
}
