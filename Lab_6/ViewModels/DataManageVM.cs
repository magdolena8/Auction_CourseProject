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
    public class DataManageVM : INotifyPropertyChanged
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
        private ObservableCollection<PRODUCTS> _userProducts;
        public ObservableCollection<PRODUCTS> UserProducts
        {
            get { return _userProducts; }
            set
            {
                _userProducts = value;
                NotifyPropertyChanged("UserProducts");
            }
        }

        public DataManageVM(USERS user)
        {
            User = user;
            AllProducts = DataWorker.GetAllProducts();
            UserProducts = DataWorker.GetUserProducts(user);
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
                    OpenNewProductInfoWNDMethod((PRODUCTS)obj);
                }
                );
            }
        }

        private RelayCommand _opnenAddNewProductWND;
        public RelayCommand OpnenAddNewProductWND
        {
            get
            {
                return _opnenAddNewProductWND ?? new RelayCommand(obj =>
                {
                    Lab_6.AddProductWindow qwe = new AddProductWindow();
                    qwe.Show();
                });
            }
        }


        #endregion


        #region OPEN NEW WINDOWS METHODS
        private void OpenNewProductInfoWNDMethod(PRODUCTS product)
        {
            MoreWindow newMoreInfoWindow = new MoreWindow();
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

        #region REFRESH VIEW AFTER DB UDATE

        private RelayCommand _updateData;
        public RelayCommand UpdateData
        {
            get
            {
                return _updateData ?? new RelayCommand(obj =>
                {
                    //obj = obj as PRODUCTS;
                    AllProducts = DataWorker.GetAllProducts();
                    //OpenNewProductInfoWNDMethod((PRODUCTS)obj);
                }
                );
            }
        }

        #endregion
    }

}

