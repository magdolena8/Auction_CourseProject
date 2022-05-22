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
using Lab_6.Model;
using Lab_6.ViewModels;



namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static observablecollection<product> productlist = new observablecollection<product>();
        //public MainWindow(DataManageVM a)
        //{

        //    InitializeComponent();
        //    //AUCTION_DBEntities db = new AUCTION_DBEntities();
        //    //db = new ViewModel();

        //    this.DataContext = a;
        //    this.DataContext = new DataManageVM();
        //    //AutentificationWindow Autentifiction = new AutentificationWindow();
        //    //Autentifiction.Show();

        //}
        public MainWindow()
        {

            InitializeComponent();
            //AUCTION_DBEntities db = new AUCTION_DBEntities();
            //db = new ViewModel();

            //this.DataContext = a;
            //AutentificationWindow Autentifiction = new AutentificationWindow();
            //Autentifiction.Show();

        }
    }
}
