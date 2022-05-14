using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Microsoft.SqlServer.Server;
using System.Data.Entity;



namespace Lab_6
{
    internal partial class ViewModel : INotifyPropertyChanged
    {
        private Product _selectedProsuct;
        private ObservableCollection<Product> _productList;

        public ObservableCollection<Product> Products
        {
            get { return _productList; }
            set
            {
                _productList = value;
                OnPropertyChanged("ProductList");
            }
        }
        public Product SelectedProduct
        {
            get { return _selectedProsuct; }
            set
            {
                _selectedProsuct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }
        public ViewModel()
        {
            Products = new ObservableCollection<Product>();
            SelectedProduct = null;
            Serialization.ProductSerializer.Deserialize(Serialization.ProductSerializer.XMLDataPath, Products);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
