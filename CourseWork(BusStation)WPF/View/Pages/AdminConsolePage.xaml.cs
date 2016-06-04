using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CourseWork_BusStation_WPF.ViewModel;

namespace CourseWork_BusStation_WPF.View.Pages
{
    /// <summary>
    /// Interaction logic for AdminConsolePage.xaml
    /// </summary>
    public partial class AdminConsolePage : Page
    {
        public AdminConsolePage()
        {
            InitializeComponent();
            this.DataContext = new AdminConsoleViewModel(this);
        }
    }
}
