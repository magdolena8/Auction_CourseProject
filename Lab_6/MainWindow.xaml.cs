using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Product> ProductList = new ObservableCollection<Product>();
        public MainWindow()
        {
            InitializeComponent();
            ProductList.Add(new Product("qwe", "1231", DateTime.Now, 4, "description", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));

            Products.ItemsSource = ProductList;
        }


        private void MoreAbout(object sender, RoutedEventArgs e)
        {
            MoreWindow moreWindow = new MoreWindow(ProductList[Products.SelectedIndex]);
            moreWindow.Show();
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            AddProductWindow moreWindow = new AddProductWindow();
            moreWindow.Show();
        }
    }
}
