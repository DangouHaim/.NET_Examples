using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TicketManagement.WPF.Util
{
    class LocalizedErrorManager
    {
        private static object _tlock = new object();
        private static LocalizedErrorManager _instance;

        public string InsertError { get; private set; }
        public string ChangeUserError { get; private set; }
        public string RoleError { get; private set; }
        public string LoginError { get; private set; }
        public string AccessDenied { get; private set; }

        public LocalizedErrorManager()
        {
            App.LanguageChanged += (s, e) =>
            {
                LoadErrorResources();
            };
            LoadErrorResources();
        }

        private void LoadErrorResources()
        {
            var r = from x in Application.Current.Resources.MergedDictionaries select x;
            if (r.Any())
            {
                foreach (var v in r)
                {
                    InsertError = v["InsertError"].ToString();
                    if (!string.IsNullOrEmpty(InsertError))
                    {
                        break;
                    }
                }
                foreach (var v in r)
                {
                    ChangeUserError = v["ChangeUserError"].ToString();
                    if (!string.IsNullOrEmpty(ChangeUserError))
                    {
                        break;
                    }
                }
                foreach (var v in r)
                {
                    RoleError = v["RoleError"].ToString();
                    if (!string.IsNullOrEmpty(RoleError))
                    {
                        break;
                    }
                }
                foreach (var v in r)
                {
                    LoginError = v["LoginError"].ToString();
                    if (!string.IsNullOrEmpty(LoginError))
                    {
                        break;
                    }
                }
                foreach (var v in r)
                {
                    AccessDenied = v["AccessDenied"].ToString();
                    if (!string.IsNullOrEmpty(AccessDenied))
                    {
                        break;
                    }
                }
            }
        }

        public static LocalizedErrorManager GetInstance()
        {
            if (_instance == null)
            {
                lock(_tlock)
                {
                    if(_instance == null)
                    {
                        _instance = new LocalizedErrorManager();
                    }
                }
            }
            return _instance;
        }
    }
}
