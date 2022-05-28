using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using Lab_6.Model;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Globalization;

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
        //public CollectionView ProductsView { get; private set; }



        private ObservableCollection<PRODUCTS> _searchResult;
        public ObservableCollection<PRODUCTS> SearchResult
        {
            get { return _searchResult; }
            set
            {
                _searchResult = value;
                NotifyPropertyChanged("SearchResult");
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
        private ObservableCollection<PRODUCTS> _userBasket = DataWorker.GetUserBasket(UserDataWorker.CurrentUser);
        public ObservableCollection<PRODUCTS> UserBasket
        {
            get { return _userBasket; }
            set
            {
                _userBasket = value;
                NotifyPropertyChanged("UserBasket");
            }
        }

        public DataManageVM(USERS user)
        {
            User = user;
            AllProducts = DataWorker.GetAllProducts();
        }
        public DataManageVM()
        {
            User = null;
            AllProducts = DataWorker.GetAllProducts();
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
            if (UserDataWorker.CurrentUser == null)
            {
                MessageBox.Show("Требуется регистрация или вход");
                return;
            }
            if ((UserDataWorker.CurrentUser.USER_TYPE != "admin") && (product.OWNER_ID != UserDataWorker.CurrentUser.ID_USER))
            {
                Button deleteButton = (Button)newMoreInfoWindow.FindName("deleteBtn");
                deleteButton.Visibility = Visibility.Collapsed;
                Button editButton = (Button)newMoreInfoWindow.FindName("editBtn");
                editButton.Visibility = Visibility.Collapsed;
            }
            if (UserDataWorker.CurrentUser.USER_TYPE == "admin" || product.OWNER_ID == UserDataWorker.CurrentUser.ID_USER)
            {
                //TextBox newBidTextBlock = (TextBox)newMoreInfoWindow.FindName("newBidTextBlock");
                //newBidTextBlock.Visibility = Visibility.Collapsed;
                //Button placePidBtn = (Button)newMoreInfoWindow.FindName("placePidBtn");
                //placePidBtn.Visibility = Visibility.Collapsed;
                StackPanel BidPanel = (StackPanel)newMoreInfoWindow.FindName("BidPanel");
                BidPanel.Visibility = Visibility.Collapsed;
            }
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
                    if (UserDataWorker.CurrentUser != null)
                    {
                        UserBasket = DataWorker.GetUserBasket(UserDataWorker.CurrentUser);
                        UserProducts = DataWorker.GetUserProducts(UserDataWorker.CurrentUser);
                    }
                    AllProducts = DataWorker.GetAllProducts();

                }
                );
            }
        }
        #endregion

        private RelayCommand _searchProducts;
        public RelayCommand SearchProducts
        {
            get
            {
                return _searchProducts ?? new RelayCommand(obj =>
                {
                    //obj = obj as string;
                    AllProducts = DataWorker.FindProducts((string)obj);
                    //AllProducts.
                });
            }
        }
        private RelayCommand _changeLanguage;
        public RelayCommand ChangeLanguage
        {
            get
            {
                return _changeLanguage ?? new RelayCommand(obj =>
                {
                    ComboBoxItem langComboBox = obj as ComboBoxItem;
                    string lang = langComboBox.Content.ToString();
                    WNDManager.ChangeLanguage(lang);
                }
                );
            }
        }

    }

}

