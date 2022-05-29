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
        //public string USER_AVATAR_LINK
        //{
        //    get
        //    {
        //        return UserDataWorker.GetUserAvatarLink(this.User);
        //    }
        //}
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
                    if (passBox1.Text != "")
                    {

                        USERS registrationResult = UserDataWorker.RegisterUser(this.User);
                        if (registrationResult != null)
                        {
                            WNDManager.openUserMainWindow(autWnd, registrationResult);
                        }
                        else
                        {
                            passBox1.Background = Brushes.Red;
                            MessageBox.Show("Неверный Логин или Пароль");
                        }
                    }
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

        private RelayCommand _editImage;
        public RelayCommand EditImage
        {
            get
            {
                return _editImage ?? new RelayCommand(obj =>
                {
                    string imagePath;
                    OpenFileDialog editImageFileDialog = new OpenFileDialog();
                    if (editImageFileDialog.ShowDialog() == true)
                    {
                        imagePath = editImageFileDialog.FileName;
                        try
                        {

                            this.User.USER_AVATAR_LINK = imagePath;
                            //this.Product.PRODUCT_IMAGE_LINK = DataWorker.SetImageLinkToProduct(this.Product, imagePath);
                            NotifyPropertyChanged("User");
                            MessageBox.Show("ImageEdited");

                        }
                        catch (NotSupportedException ex)
                        {
                            MessageBox.Show("Неверный формат");
                        }
                    }
                });
            }
        }

        private RelayCommand _editUser;
        public RelayCommand EditUser
        {
            get
            {
                return _editUser ?? new RelayCommand(obj =>
                {
                    try
                    {
                        UserVM userVM = obj as UserVM;
                        USERS user = userVM.User;
                        USERS updatedUser = UserDataWorker.EditUser(user.ID_USER, user);
                        if (updatedUser != null)
                        {
                            user = updatedUser;
                            //wnd.BeginInit();
                        }
                        else
                        {
                            MessageBox.Show("Возникла ошибка");
                        }
                    }
                    catch
                    {
                        //SetRedBlockControll(wnd, bidTextBox);
                        MessageBox.Show("Ошибка");
                        return;
                    }

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
