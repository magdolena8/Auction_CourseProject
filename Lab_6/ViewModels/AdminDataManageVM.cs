using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using Lab_6.Model;

namespace Lab_6.ViewModels
{
    public class AdminDataManageVM : INotifyPropertyChanged
    {
        private USERS _user;
        public USERS User
        {
            get { return _user; }
            set
            {
                _user = value;
                NotifyPropertyChanged("User");
            }
        }

        //Получить все продукты
        private ObservableCollection<PRODUCTS> _allProducts = DataWorker.GetAllProducts();
        public ObservableCollection<PRODUCTS> AllProducts
        {
            get { return _allProducts; }
            set
            {
                _allProducts = value;
                NotifyPropertyChanged("AllProducts");
            }
        }
        private ObservableCollection<USERS> _allUsers;
        public ObservableCollection<USERS> AllUsers
        {
            get { return _allUsers; }
            set
            {
                _allUsers = value;
                NotifyPropertyChanged("AllUsers");
            }
        }

        public AdminDataManageVM(USERS user)
        {
            User = user;
            AllProducts = DataWorker.GetAllProducts();
            AllUsers = DataWorker.GetAllUsers();

        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region OPEN NEW WINDOWS COMMANDS
        private RelayCommand _openProductInfoWnd;
        public RelayCommand OpenProductInfoWnd
        {
            get
            {
                return _openProductInfoWnd ?? new RelayCommand(obj =>
                {
                    //obj = obj as PRODUCTS;
                    OpenAdminProductInfoWNDMethod((PRODUCTS)obj);
                }
                );
            }
        }
        #endregion


        #region OPEN NEW WINDOWS METHODS
        private void OpenAdminProductInfoWNDMethod(PRODUCTS product)
        {
            AdminProductInfo newMoreInfoWindow = new AdminProductInfo();
            SetCentrePositionAndOpenWnd(newMoreInfoWindow);
            newMoreInfoWindow.DataContext = new ProductVM(product);
        }
        private void SetCentrePositionAndOpenWnd(Window wnd)
        {
            wnd.Owner = Application.Current.MainWindow;
            wnd.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            wnd.Show();
        }

        #endregion

        //#region REFRESH VIEW AFTER DB UDATE

        private RelayCommand _updateData;
        public RelayCommand UpdateData
        {
            get
            {
                return _updateData ?? new RelayCommand(obj =>
                {
                    AllProducts = DataWorker.GetAllProducts();
                    AllUsers = DataWorker.GetAllUsers();
                }
                );
            }
        }
    }
}
