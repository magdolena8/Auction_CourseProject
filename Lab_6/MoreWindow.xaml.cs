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
using Microsoft.Win32;


namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для MoreWindow.xaml
    /// </summary>
    public partial class MoreWindow : Window
    {
        private Product _product;
        string photoPath = "";
        public MoreWindow(Product product)
        {
            InitializeComponent();
            _product = product;
            this.DataContext = this._product;
            try
            {
                //Title.Text = _product.Title.ToString();
                //Price.Text = _product.Price.ToString();
                //TimeLeft.Text = _product.TimeLeft.ToString();
                //Bids.Text = _product.Bids.ToString();
                //Type.Text = _product.Type.ToString();
                //Description.Text = _product.Description.ToString();
                //Image.Source = new ImageSourceConverter().ConvertFromString(product.Image) as ImageSource;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        public MoreWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        private void deleteProduct(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Удалить этот товар ?", "Удаление", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //MainWindow.ProductList.Remove(_product);
                this.Close();
            }

        }
        private void editProduct(object sender, RoutedEventArgs e)
        {
            Title.IsReadOnly = false;
            Price.IsReadOnly = false;
            TimeLeft.IsReadOnly = false;
            Bids.IsReadOnly = false;
            Type.IsReadOnly = false;
            doneEditBtn.Visibility = Visibility.Visible;
            editImageBtn.Visibility = Visibility.Visible;
        }
        private void editPhoto(object sender, RoutedEventArgs e)
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
        private void doneEdit(object sender, RoutedEventArgs e)
        {
            try
            {
                //validation
            }
            catch
            {
                MessageBox.Show("Error"); return;
            }
            doneEditBtn.Visibility = Visibility.Hidden;
            MessageBox.Show("DONE");
        }
    }

}
