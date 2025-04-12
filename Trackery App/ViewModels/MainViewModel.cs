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

namespace Trackery_App.ViewModels
{
    internal class MainViewModel : ObservableObject
    {


        public ICommand HomeViewCommand { get; set; }
        public ICommand DiscoveryViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public DiscoveryViewModel DiscoveryVM { get; set; }
        private IUserRepository _userRepository;
        private object _currentView;
        private UserAccountModel _currentUserAccount;
        public UserAccountModel CurrentUserAccount
        {
            get { return _currentUserAccount; }
            set
            {
                _currentUserAccount = value;
                OnPropertyChanged();
            }
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
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });

        }
        private void LoadCurrentUserAccount()
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            var user = _userRepository.GetUserByUsername(username);
            if (user != null)
            {
                CurrentUserAccount = new UserAccountModel()
                {
                    Username = user.Username,
                    ProfilePicture = null
                };
            }
            else
            {
                MessageBox.Show("User not found.");
                Application.Current.Shutdown();
            }

        }
    }
}
