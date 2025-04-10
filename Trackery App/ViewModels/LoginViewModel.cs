using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Trackery_App.Core;
using Trackery_App.Infrastructure.Repositories;

namespace Trackery_App.ViewModels
{
    internal class LoginViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        private string _username;
        private SecureString _password;
        private string _errorMessage;
        private IUserRepository _userRepository;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
            }
        }
        public SecureString Password
        {
            get { return _password; }
            set
            {
                _password = value;
            }
        }
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoginCommand { get; set; }
        public ICommand RecoverPasswordCommand { get; set; }
        public ICommand ShowPasswordCommand { get; set; }
        public ICommand RememberPasswordCommand { get; set; }
        public LoginViewModel(INavigationService navigationService)
        {
            _userRepository = new UserRepository();
            _navigationService = navigationService;
            LoginCommand = new AsyncRelayCommand(async o =>
            {
                if (!CanExecuteLogin())
                {
                    ErrorMessage = "* Username and password should be longer than 3 characthers.";
                }
                else if (!await LoginAsync())
                {
                    ErrorMessage = "* Invalid username or password.";
                }
                else
                {
                    NavigateToMainView();
                }
            },
            onException: ex => ErrorMessage = ex.Message);
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
        private bool CanExecuteLogin()
        {
            return Username != null && Password != null && Password.Length > 3 && Username.Length > 3;
        }
        private async Task<bool> LoginAsync()
        {
            bool IsValid = await _userRepository.AuthenticateUserAsync(new NetworkCredential(Username, Password));
            if (IsValid)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                return IsValid;
            }
            return false;
        }
        private void NavigateToMainView()
        {
            _navigationService.OpenMainWindow();
            _navigationService.CloseLoginView();
        }
    }
}
