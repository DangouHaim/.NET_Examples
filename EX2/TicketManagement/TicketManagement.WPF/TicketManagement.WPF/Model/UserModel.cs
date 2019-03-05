using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Security;

namespace TicketManagement.WPF.Model
{
    class UserModel : INotifyPropertyChanged
    {
        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private bool _loginError;
        public bool LoginError
        {
            get => _loginError;
            set
            {
                _loginError = value;
                OnPropertyChanged();
                OnPropertyChanged("LoginColor");
            }
        }

        private bool _emailError;
        public bool EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChanged();
                OnPropertyChanged("EmailColor");
            }
        }

        private Brush _emailColor = Brushes.Black;
        public Brush EmailColor
        {
            get
            {
                if(_emailError)
                {
                    return Brushes.Red;
                }
                return _emailColor;
            }
            set
            {
                _emailColor = value;
                OnPropertyChanged();
            }
        }

        private Brush _loginColor = Brushes.Black;
        public Brush LoginColor
        {
            get
            {
                if(_loginError)
                {
                    return Brushes.Red;
                }
                return _loginColor;
            }
            set
            {
                _loginColor = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _roles;
        public ObservableCollection<string> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = (s, e) => { };//null object
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
