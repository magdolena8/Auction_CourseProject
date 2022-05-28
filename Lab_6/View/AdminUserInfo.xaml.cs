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
    /// Логика взаимодействия для AdminUserInfo.xaml
    /// </summary>
    public partial class AdminUserInfo : Window
    {
        public AdminUserInfo()
        {
            InitializeComponent();
        }
        private void editBtnClick(object sender, RoutedEventArgs e)
        {
            IdUser.IsReadOnly = false;
            UserLogin.IsReadOnly = false;
            RightsUser.IsReadOnly = false;
            IDUser.IsReadOnly = false;

            doneEditBtn.Visibility = Visibility.Visible;
            editImageBtn.Visibility = Visibility.Visible;

        }
    }
}
