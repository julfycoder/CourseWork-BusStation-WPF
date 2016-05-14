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
using System.Data;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;

namespace CourseWork_BusStation_WPF.View.Pages
{
    /// <summary>
    /// Interaction logic for FlightsPreviewPage.xaml
    /// </summary>
    public partial class FlightsPreviewPage : Page
    {
        public FlightsPreviewPage()
        {
            InitializeComponent();
        }

        private void BackToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
