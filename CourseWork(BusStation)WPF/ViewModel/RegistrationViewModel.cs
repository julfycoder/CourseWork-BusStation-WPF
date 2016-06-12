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
using System.Security.Cryptography;
using System.Windows;

namespace CourseWork_BusStation_WPF.ViewModel
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        const int correctPasswordLength = 6;
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
                Password=GetMD5Hash(Password),
                Login=Login
            };
            station.AddEntity<Administrator>(admin);
        }
        string GetMD5Hash(string input)
        {
            MD5 md = new MD5CryptoServiceProvider();
            byte[] data = md.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++) sBuilder.Append(data[i]);
            return sBuilder.ToString();
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
            if ((Surname != null && Surname != "" && Surname.Substring(0, 1) != " ") &&
                (Name != null && Name != "" && Name.Substring(0, 1) != " ") &&
                (Login != null && Login != "" && Login.Substring(0, 1) != " ") &&
                (Password != null && Password != "" && Password.Substring(0, 1) != " "))
            {
                if (Password.Length > correctPasswordLength)
                {
                    List<Administrator> admins = station.GetEntities<Administrator>();
                    foreach (Administrator admin in admins)
                    {
                        if (admin.Login == Login && admin.Password == GetMD5Hash(Password))
                        {
                            MessageBox.Show("Пользователь с таким сочетанием логина и пароля уже существует!", "Ошибка регистрации", MessageBoxButton.OK, MessageBoxImage.Warning);
                            return;
                        }
                    }
                    Registration();
                    BackToMainMenu();
                }
                else MessageBox.Show("Для сохранения безопасности, \nваш пароль должен содержать более 6 символов!", "Длина пароля не корректна", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else MessageBox.Show("Все поля должны быть заполнены\n и данные в полях не должны начинаться на пробел!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
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
