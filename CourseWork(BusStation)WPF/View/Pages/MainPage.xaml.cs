﻿using System;
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
using CourseWork_BusStation_WPF.View.Pages;

namespace CourseWork_BusStation_WPF
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void EnterLikeAGuest_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new FlightsPreviewPage());
        }
    }
}
