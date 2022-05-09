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

namespace Lab_6
{
    /// <summary>
    /// Логика взаимодействия для MoreWindow.xaml
    /// </summary>
    public partial class MoreWindow : Window
    {

        public MoreWindow(Product product)
        {
            try
            {
                InitializeComponent();
                Title.Text = product.Title.ToString();
                Price.Text = product.Price.ToString();
                TimeLeft.Text = product.TimeLeft.ToString();
                Bids.Text = product.Bids.ToString();
                Type.Text = product.Type.ToString();
                Description.Text = product.Description.ToString();
                Image.Source = new ImageSourceConverter().ConvertFromString(product.Image) as ImageSource;

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

        private void Description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
