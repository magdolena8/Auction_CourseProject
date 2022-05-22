using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6.ViewModels;
using Lab_6.Model;
using System.Diagnostics;
using System.Windows;

namespace Lab_6
{
    public static class WNDManager
    {
        public static void openUserMainWindow(AutentificationWindow autWnd, USERS user)
        {
            Console.WriteLine("Opened window for common user" + user.LOGIN_USER.ToString());
            MainWindow mainWND = new MainWindow();
            mainWND.DataContext = new DataManageVM(user);
            autWnd.Close();
            Application.Current.MainWindow = mainWND;
            mainWND.Show();
        }


        public static void openAdminMainWindow(AutentificationWindow autWnd, USERS user)
        {
            Console.WriteLine("Opened window for common user" + user.LOGIN_USER.ToString());
            AdminMainWindow mainWND = new AdminMainWindow();
            mainWND.DataContext = new AdminDataManageVM(user);
            autWnd.Close();
            Application.Current.MainWindow = mainWND;
            mainWND.Show();
        }
    }
}
