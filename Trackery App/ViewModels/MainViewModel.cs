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
        public ICommand StockViewCommand { get; set; }
        public ICommand DeliveriesViewCommand { get; set; }
        public ICommand EmployeesViewCommand { get; set; }
        public ICommand UserSettingsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public StockViewModel StockVM { get; set; }
        public DeliveriesViewModel DeliveriesVM { get; set; }
        public EmployeesViewModel EmployeesVM { get; set; }
        public UserSettingsViewModel UserSettingsVM { get; set; }
        private IStockRepository _stockRepository;
        public ObservableCollection<StockModel> Stock
        {
            get;
            set;
        }
        private IDeliveryRepository _deliveryRepository;
        public ObservableCollection<DeliveryModel> Deliveries
        {
            get;
            set;
        }
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
        private bool _isRoleChangeEnabled;
        public bool IsRoleChangeEnabled
        {
            get => _isRoleChangeEnabled;
            set
            {
                _isRoleChangeEnabled = value;
                OnPropertyChanged();
            }
        }
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
            _deliveryRepository = new DeliveryRepository();
            _stockRepository = new StockRepository();
            LoadEmployeesData();
            LoadDeliveriesData();
            LoadStockData();
            _picturePath = $"/Images/{_role}-avatar.png";
            HomeVM = new HomeViewModel();
            StockVM = new StockViewModel();
            DeliveriesVM = new DeliveriesViewModel();
            EmployeesVM = new EmployeesViewModel();
            UserSettingsVM = new UserSettingsViewModel();
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });
            StockViewCommand = new RelayCommand(o =>
            {
                CurrentView = StockVM;
            });
            DeliveriesViewCommand = new RelayCommand(o =>
            {
                CurrentView = DeliveriesVM;
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


        public void LoadEmployeesData()
        {
            var allUsers = _userRepository.GetAllUsers();
            Employees = new ObservableCollection<UserModel>(allUsers);
            foreach (var user in allUsers)
            {
                user.PropertyChanged += OnUserModelChanged;
                if (user.Username.ToLower() == Thread.CurrentPrincipal.Identity.Name.ToLower())
                {
                    _currentUser = user;
                }
            }
            LoadCurrentUserAccount();
        }
        private void LoadCurrentUserAccount()
        {
            if (_currentUser != null)
            {
                CurrentUsername = _currentUser.Username;
                _role = _currentUser.Role;
                ResolveRoleSpecifics();
            }
            else
            {
                MessageBox.Show("User not found.");
                Application.Current.Shutdown();
            }

        }
        private void ResolveRoleSpecifics()
        {

            switch (_currentUser.Role)
            {
                case "admin":
                    IsRoleChangeEnabled = true;
                    break;
                case "boss":
                    IsRoleChangeEnabled = true;
                    break;
                case "employee":
                    IsRoleChangeEnabled = false;
                    break;
                default:
                    IsRoleChangeEnabled = false;
                    break;
            }
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

        private void LoadDeliveriesData()
        {
            Deliveries = new ObservableCollection<DeliveryModel>(_deliveryRepository.GetDeliveries());
        }
        private void LoadStockData()
        {
            Stock = new ObservableCollection<StockModel>(_stockRepository.GetStock());
        }
    }
}
