using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TicketManagement.WPF.View
{
    /// <summary>
    /// Логика взаимодействия для TicketManagementView.xaml
    /// </summary>
    public partial class TicketManagementView : Window
    {
        public TicketManagementView()
        {
            var r = new LoginView().ShowDialog();
            if (r == null || !r.Value)
            {
                Close();
            }
            InitializeComponent();
            DataContext = new ManagementViewModel();
        }
    }
}
