using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Controls;
using CourseWork_BusStation_WPF.Commands;
using CourseWork_BusStation_WPF.Model.AuthentificationEntity;
using CourseWork_BusStation_WPF.Model;
using System.Reflection;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        IAccessible station;
        Page currentPage;
        public RegistrationViewModel(Page page)
        {
            this.currentPage = page;
            BackToMainMenuCommand = new Command(arg => BackToMainMenu());
            ApplyRegistrationCommand = new Command(arg => ApplyRegistration());
            station = new BusStationAccess();
        }
        void Registration()
        {
            Administrator admin = new Administrator() 
            {
                Surname=Surname,
                Name=Name,
                Password=Password,
                Login=Login
            };
            station.AddEntity<Administrator>(admin);
        }
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        void UpdatePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand BackToMainMenuCommand { get; set; }
        void BackToMainMenu()
        {
            currentPage.NavigationService.Navigate(new MainPage());
        }
        public ICommand ApplyRegistrationCommand { get; set; }
        void ApplyRegistration()
        {
            Registration();
            BackToMainMenu();
        }
        #endregion

        #region Properties
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        #endregion
    }
}
