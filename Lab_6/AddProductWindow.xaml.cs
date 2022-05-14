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
using System.Windows.Shapes;
using System.Windows.Input;
using Microsoft.Win32;

namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private string photoPath = "";
        public AddProductWindow()
        {
            InitializeComponent();
        }
        private void UplodPhoto(object sender, RoutedEventArgs e)
        {
            OpenFileDialog addPhotoFileDialog = new OpenFileDialog();
            if (addPhotoFileDialog.ShowDialog() == true)
            {
                photoPath = addPhotoFileDialog.FileName;
            }
            try
            {
                Image.Source = new ImageSourceConverter().ConvertFromString(photoPath) as ImageSource;
            }
            catch (NotSupportedException ex)
            {
                MessageBox.Show("Неверный формат");
            }
        }
        //private void AddUserProduct(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        try
        //        {
        //            Product userProduct = new Product(Title.Text, Price.Text, (DateTime)TimePicker.SelectedDate, 0, Description.Text, Type.Text, photoPath);
        //            MainWindow.ProductList.Add(userProduct);
        //            Serialization.ProductSerializer.Serialize(MainWindow.ProductList, Serialization.ProductSerializer.XMLDataPath);
        //        } 
        //        catch
        //        {
        //            MessageBox.Show("Date error"); return;
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show("Error"); return;
        //    }
        //    MessageBox.Show("DONE");
        //    //AddProductWindow moreWindow = new AddProductWindow();
        //}

    }
}
