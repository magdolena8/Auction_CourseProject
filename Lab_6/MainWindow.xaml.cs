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
using System.Data.Entity;
using Lab_6.Models;


namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel = null;
        //public static observablecollection<product> productlist = new observablecollection<product>();
        public MainWindow()
        {

            InitializeComponent();
            //AUCTION_DBEntities db = new AUCTION_DBEntities();
            //db = new ViewModel();




            ////////////ХУЕТА////////////////
            AUCTION_DBEntities db = new AUCTION_DBEntities();
            var users = db.USERS;
            foreach (var user in users)
            {
                Console.WriteLine(user.PASSWORD_USER.ToString());
            }


            ////////////ХУЕТА////////////////




            //ProductList.Add(new Product("qwe", "1231", DateTime.Now, 4, "description", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\free-icon-font-cross-3917759.png.jpg"));
            //ProductList.Add(new Product("tyujnhbgf", "56543", DateTime.Now, 4, "dvfhnhn", "wefsf", @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Media\asscreed.jpg"));
            //Serialization.ProductSerializer.Deserialize(Serialization.ProductSerializer.XMLDataPath, DataContext);
            //Products.ItemsSource = ProductList;
        }


        public void MoreAbout(object sender, RoutedEventArgs e)
        {
            MoreWindow moreWindow = new MoreWindow(_viewModel.SelectedProduct);
            moreWindow.Show();
        }
        private void AddProduct(object sender, RoutedEventArgs e)
        {
            AddProductWindow moreWindow = new AddProductWindow();
            moreWindow.Show();
        }
    }
}
