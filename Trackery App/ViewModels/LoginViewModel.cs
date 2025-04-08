using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackery_App.Core;

namespace Trackery_App.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;

        private string _username;
        private SecureString _password;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
            }
        }
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }
        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(o =>
            {
                _navigationService.OpenMainWindow();
                _navigationService.CloseLoginView();
            });
            RecoverPasswordCommand = new RelayCommand(o =>
            {
                // Implement password recovery logic here
            });
            ShowPasswordCommand = new RelayCommand(o =>
            {
            });
            RememberPasswordCommand = new RelayCommand(o =>
            {
                // Implement remember password logic here
            });
        }
        private bool CanExecuteLogin(object parameter)
        {
            return !string.IsNullOrEmpty(Username) && Password != null && Password.Length > 3 && Username.Length > 3;
        }
    }
}
