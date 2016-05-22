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
using System.Data;

namespace CourseWork_BusStation_WPF.View.Pages
{
    /// <summary>
    /// Interaction logic for TicketReservationPage.xaml
    /// </summary>
    public partial class TicketReservationPage : Page
    {
        public TicketReservationPage(DataRow flight)
        {
            InitializeComponent();
            this.DataContext = new TicketReservationViewModel(this, flight);
        }
    }
}
