using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TicketManagement.WPF.ViewModel;
using TicketManagement.WPF.WCF.AreaService;

namespace TicketManagement.WPF.View
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            AreaServiceClient c = new AreaServiceClient();
            c.ClientCredentials.UserName.UserName = "";
            c.ClientCredentials.UserName.Password = "";
            c.Open();
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}
