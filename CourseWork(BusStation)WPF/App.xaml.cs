using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using CourseWork_BusStation_WPF.ViewModel;

namespace CourseWork_BusStation_WPF
{

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var window = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            window.Show();
        }
    }
}
