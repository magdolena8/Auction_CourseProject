using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Lab_6.Model;
using Microsoft.Win32;


namespace Lab_6.ViewModels
{
    public class ProductVM : INotifyPropertyChanged
    {
        private PRODUCTS _product;
        public PRODUCTS Product
        {
            get
            {
                return _product;
            }
            set
            {
                _product = value;
                NotifyPropertyChanged("Product");
            }
        }
        //public bool? isUserTopBid
        //{
        //    get
        //    {
        //        if (Product.ENDTIME < DateTime.Now)
        //        {
        //            return null;
        //        }
        //        return Product.TOP_BID_USER_ID == UserDataWorker.CurrentUser.ID_USER;
        //    }
        //    set
        //    {
        //        isUserTopBid = value;
        //        NotifyPropertyChanged("isUserTopBid");
        //    }
        //}
        public ProductVM()
        {
            Product = new PRODUCTS();
            Product.TITLE = "";
            Product.ENDTIME = DateTime.Now;
            Product.PRICE = 0;
            NotifyPropertyChanged("Product");
        }
        public ProductVM(PRODUCTS obj)
        {
            Product = obj;
            //isUserTopBid = obj.TOP_BID_USER_ID == UserDataWorker.CurrentUser.ID_USER;
        }
        private RelayCommand _placeBid;
        public RelayCommand PlaceBid
        {
            get
            {
                return _placeBid ?? new RelayCommand(obj =>
                {
                    Window wnd = obj as Window;
                    TextBox bidTextBox = wnd.FindName("newBidTextBlock") as TextBox;
                    if (bidTextBox.Text == null || bidTextBox.Text.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControll(wnd, bidTextBox);
                        return;
                    }
                    else
                    {
                        try
                        {
                            var mainContext = Application.Current.MainWindow.DataContext;

                            string resultStr;
                            decimal bid = Convert.ToDecimal(bidTextBox.Text);
                            PRODUCTS updatedProduct = DataWorker.PlaceBid(this.Product, bid, UserDataWorker.CurrentUser);
                            if (updatedProduct != null)
                            {
                                this.Product = updatedProduct;
                            }
                            else
                            {
                                MessageBox.Show("Возникла ошибка");
                            }

                        }
                        catch
                        {
                            SetRedBlockControll(wnd, bidTextBox);
                            MessageBox.Show("Ошибка");
                            return;
                        }
                    }
                });
            }

        }
        private RelayCommand _addToBasket;
        public RelayCommand AddToBasket
        {
            get
            {
                return _addToBasket ?? new RelayCommand(obj =>
                {
                    PRODUCTS product = obj as PRODUCTS;
                    DataWorker.AddProductToBasket(UserDataWorker.CurrentUser.ID_USER, product);
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

                            this.Product.PRODUCT_IMAGE_LINK = imagePath;
                            //this.Product.PRODUCT_IMAGE_LINK = DataWorker.SetImageLinkToProduct(this.Product, imagePath);
                            NotifyPropertyChanged("Product");
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
        private RelayCommand _editProduct;
        public RelayCommand EditProduct
        {
            get
            {
                return _editProduct ?? new RelayCommand(obj =>
                {
                    try
                    {
                        ProductVM productVM = obj as ProductVM;
                        PRODUCTS product = productVM.Product;
                        PRODUCTS updatedProduct = DataWorker.EditProduct(product.ID_PRODUCT, product);
                        if (updatedProduct != null)
                        {
                            product = updatedProduct;
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

        private RelayCommand _deleteProduct;
        public RelayCommand DeleteProduct
        {
            get
            {
                return _deleteProduct ?? new RelayCommand(obj =>
                {
                    try
                    {
                        ProductVM productVM = obj as ProductVM;
                        PRODUCTS product = productVM.Product;
                        //string result = DataWorker.DeleteProduct(product);
                        if (DataWorker.DeleteProduct(product))
                        {
                            MessageBox.Show("Товар Успешно удален");
                            //product = updatedProduct;
                            //wnd.BeginInit();
                            product = null;

                            NotifyPropertyChanged("UserProducts");

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

        private RelayCommand _addNewProduct;
        public RelayCommand AddNewProduct
        {
            get
            {
                return _addNewProduct ?? new RelayCommand(obj =>
                {
                    try
                    {
                        PRODUCTS product = obj as PRODUCTS;
                        PRODUCTS addedProduct = DataWorker.CreateProduct(product);
                        //PRODUCTS updatedProduct = DataWorker.EditProduct(this.Product.ID_PRODUCT, this.Product);
                        if (addedProduct != null)
                        {
                            this.Product = addedProduct;
                            MessageBox.Show("Done");
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



        //private RelayCommand _opnenAddNewProductWND;
        //public RelayCommand OpnenAddNewProductWND
        //{
        //    get
        //    {
        //        return _opnenAddNewProductWND ?? new RelayCommand(obj =>
        //        {
        //            Lab_6.AddProductWindow qwe = new AddProductWindow();
        //            qwe.Show();
        //        });
        //    }
        //}


        private void SetRedBlockControll(Window wnd, TextBox blockName)
        {
            //Control block = wnd.FindName(blockName) as Control;
            blockName.BorderBrush = System.Windows.Media.Brushes.Red;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
