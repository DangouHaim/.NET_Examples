using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Claims;
using System.Threading;
using System.Windows.Controls;
using Microsoft.Rest;
using TicketManagement.WPF.Command;
using TicketManagement.WPF.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using TicketManagement.WPF.WebAPI;
using TicketManagement.WPF.WebAPI.Models;
using TicketManagement.WPF.Util;

namespace TicketManagement.WPF.ViewModel
{
    class LoginViewModel : INotifyPropertyChanged
    {
        public static IPrincipal CurrentPrincipal;

        private readonly WebAPIClient _userApi;

        private LocalizedErrorManager _errorManager = LocalizedErrorManager.GetInstance();

        public LoginViewModel()
        {
            Login = new UserModel();
            _userApi = new WebAPIClient(new Uri("https://localhost:44319/"), new BasicAuthenticationCredentials());
        }

        private UniversalCommand _loginCommand;
        public UniversalCommand LoginCommand
        {
            get
            {
                return _loginCommand ??
                       (_loginCommand = new UniversalCommand(async x =>
                           {
                               if (x is PasswordBox box)
                               {
                                   var r = await _userApi.ApiAccountLoginPostAsync(new LoginModel(
                                       Login.Login,
                                       box.Password));
                                   if (!string.IsNullOrEmpty(r))
                                   {
                                       
                                       var t = r.Split(new[] { '"', ':' })[4];

                                       var jwtToken = new JwtSecurityToken(t);
                                       var claims = jwtToken.Claims;
                                       ClaimsIdentity claim = new ClaimsIdentity(claims);
                                       var cp = new ClaimsPrincipal(claim);
                                       var transformer = new ClaimsAuthenticationManager();
                                       var newPrincipal = transformer.Authenticate(string.Empty, cp);
                                       CurrentPrincipal = newPrincipal;
                                       Thread.CurrentPrincipal = newPrincipal;
                                       if (Thread.CurrentPrincipal.IsInRole("admin"))
                                       {
                                           DialogResult = true;
                                       }
                                       else
                                       {
                                           CurrentError = _errorManager.AccessDenied;
                                       }
                                   }
                                   else
                                   {
                                       CurrentError = _errorManager.LoginError;
                                   }
                               }
                           },
                           x =>
                           {
                               if (x is PasswordBox box)
                               {
                                   return !string.IsNullOrEmpty(Login.Login) 
                                          && !string.IsNullOrEmpty(box.Password);
                               }

                               return false;
                           }));
            }
        }

        private UniversalCommand _changeLanguageCommand;
        public UniversalCommand ChangeLanguageCommand
        {
            get
            {
                return _changeLanguageCommand ??
                       (_changeLanguageCommand = new UniversalCommand(x =>
                       {
                          if(x is ComboBoxItem item)
                           {
                               string culture = "en-US";
                               if (item.Content.ToString() == "Русский")
                               {
                                   culture = "ru-RU";
                               }
                               if (item.Content.ToString() == "Беларускі")
                               {
                                   culture = "by-BE";
                               }
                               App.Language = new System.Globalization.CultureInfo(culture);
                           }
                       }, x => x is ComboBoxItem));
            }
        }

        private UserModel _login;
        public UserModel Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged();
            }
        }

        private string _currentError;
        public string CurrentError
        {
            get => _currentError;
            set
            {
                _currentError = value;
                OnPropertyChanged();
            }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get => _dialogResult;
            set
            {
                _dialogResult = value;
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
