using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using CourseWork_BusStation_WPF.Model;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.View.Pages;
using CourseWork_BusStation_WPF.Model.AuthentificationEntity;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        IAccessible station;
        Page currentPage;
        public MainViewModel(Page page)
        {
            this.currentPage = page;
            EnterLikeAGuestCommand = new Command(arg => EnterLikeAGuest());
            EnterLikeAnAdminCommand = new Command(arg => EnterLikeAnAdmin());
            RegistrationCommand = new Command(arg => Registration());
            station = new BusStationAccess();
        }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand EnterLikeAGuestCommand { get; set; }
        void EnterLikeAGuest()
        {
            currentPage.NavigationService.Navigate(new FlightsPreviewPage());
        }
        public ICommand EnterLikeAnAdminCommand { get; set; }
        void EnterLikeAnAdmin()
        {
            List<Administrator> admins = station.GetEntities<Administrator>();

            foreach (Administrator admin in admins)
            {
                if (Password == admin.Password && Login == admin.Login)
                {
                    currentPage.NavigationService.Navigate(new AdminConsolePage());
                    return;
                }
            }
            MessageBox.Show("Логин или пароль неверны!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public ICommand RegistrationCommand { get; set; }
        void Registration()
        {
            currentPage.NavigationService.Navigate(new RegistrationPage());
        }
        #endregion

        #region Properties
        public string Password { get; set; }
        public string Login { get; set; }
        #endregion
    }
}
