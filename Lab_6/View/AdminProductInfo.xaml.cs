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
    /// Логика взаимодействия для AdminProductInfo.xaml
    /// </summary>
    public partial class AdminProductInfo : Window
    {
        public AdminProductInfo()
        {
            InitializeComponent();
        }



        private void editBtnClick(object sender, RoutedEventArgs e)
        {
            Title.IsReadOnly = false;
            Type.IsReadOnly = false;
            Description.IsReadOnly = false;
            doneEditBtn.Visibility = Visibility.Visible;
            editImageBtn.Visibility = Visibility.Visible;
        }
        private void doneEditBtnClick(object sender, RoutedEventArgs e)
        {
            Title.IsReadOnly = true;
            Type.IsReadOnly = true;
            Description.IsReadOnly = true;
            doneEditBtn.Visibility = Visibility.Collapsed;
            editImageBtn.Visibility = Visibility.Collapsed;
        }
    }
}
