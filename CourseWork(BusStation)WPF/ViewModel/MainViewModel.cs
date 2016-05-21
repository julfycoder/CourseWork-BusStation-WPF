using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model.WorkingWithDatabase;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.View.Pages;
using System.Windows.Input;
using System.Data;
using System.Windows.Controls;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        Page currentPage;
        public MainViewModel(Page page)
        {
            this.currentPage = page;
            EnterLikeAGuestCommand = new Command(arg => EnterLikeAGuest());
        }

        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand EnterLikeAGuestCommand { get; set; }
        public void EnterLikeAGuest()
        {
            currentPage.NavigationService.Navigate(new FlightsPreviewPage());
        }
    }
}
