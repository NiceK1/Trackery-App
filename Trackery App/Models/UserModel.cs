using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trackery_App.Core;

namespace Trackery_App.Models
{
    public class UserModel : ObservableObject
    {
        public string Id { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        private string _email;
        public string Email {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }
        private string _lastName;
        public string LastName {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }
        private string _role;
        public string Role {
            get { return _role; }
            set
            {
                _role = value;
                OnPropertyChanged();
            }
        }
        private bool _status;
        public bool Status { 
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }
    }
}
