using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using Lab_6.Model;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;

namespace Lab_6.ViewModels
{
    public class UserVM : INotifyPropertyChanged
    {
        private USERS _user;
        public int GOODS_COUNT
        {
            get
            {
                return UserDataWorker.GetUserGoodsCount(this.User);
            }
        }
        public USERS User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
            }
        }
        public UserVM(USERS obj)
        {
            User = obj;
        }
        public UserVM()
        {
            User = new USERS();
        }



        private RelayCommand _logInUser;
        public RelayCommand LogInUser
        {
            get
            {
                return _logInUser ?? new RelayCommand(obj =>
                {
                    AutentificationWindow autWnd = obj as AutentificationWindow;
                    USERS logResult = UserDataWorker.LogInUser(this.User);
                    if (logResult != null)
                    {
                        if (logResult.USER_TYPE == "user")
                        {
                            WNDManager.openUserMainWindow(autWnd, logResult);
                            return;
                        }
                        if (logResult.USER_TYPE == "admin")
                        {
                            WNDManager.openAdminMainWindow(autWnd, logResult);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный Логин или Пароль");
                    }
                });
            }
        }
        private RelayCommand _registerUser;
        public RelayCommand RegisterUser
        {
            get
            {
                return _registerUser ?? new RelayCommand(obj =>
                {
                    AutentificationWindow autWnd = obj as AutentificationWindow;
                    TextBox passBox1 = (TextBox)autWnd.FindName("PasswordTextBox");
                    TextBox passBox2 = (TextBox)autWnd.FindName("PasswordAgainTextBox");
                    if ((passBox1.Text == passBox2.Text) && passBox1.Text != "")
                    {

                        USERS registrationResult = UserDataWorker.RegisterUser(this.User);
                        if (registrationResult != null)
                        {
                            WNDManager.openUserMainWindow(autWnd, registrationResult);
                        }
                    }
                    else MessageBox.Show("Неверный Логин или Пароль");
                    passBox2.Background = Brushes.DarkCyan;
                });
            }
        }

        private RelayCommand _loginGuest;
        public RelayCommand LoginGuest
        {
            get
            {
                return _loginGuest ?? new RelayCommand(obj =>
                {
                    AutentificationWindow autWnd = obj as AutentificationWindow;
                    WNDManager.openGuestMainWindow(autWnd);
                    return;
                });
            }

        }







        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
