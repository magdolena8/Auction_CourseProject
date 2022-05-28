using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_6.ViewModels;
using Lab_6.Model;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace Lab_6
{
    public static class WNDManager
    {
        public static void openUserMainWindow(AutentificationWindow autWnd, USERS user)
        {
            MainWindow mainWND = new MainWindow();
            mainWND.DataContext = new DataManageVM(user);
            autWnd.Close();
            Application.Current.MainWindow = mainWND;
            mainWND.Show();
        }


        public static void openAdminMainWindow(AutentificationWindow autWnd, USERS user)
        {
            AdminMainWindow mainWND = new AdminMainWindow();
            mainWND.DataContext = new AdminDataManageVM(user); 
            autWnd.Close();
            Application.Current.MainWindow = mainWND;
            mainWND.Show();
        }
        public static void openGuestMainWindow(AutentificationWindow autWnd)
        {
            MainWindow mainWND = new MainWindow();
            mainWND.DataContext = new DataManageVM();
            TabItem UserProductsTab = (TabItem)mainWND.FindName("UserProductsTab");
            UserProductsTab.Visibility = Visibility.Collapsed;
            TabItem TrackedTab = (TabItem)mainWND.FindName("TrackedTab");
            TrackedTab.Visibility= Visibility.Collapsed;
            TabItem ProfileTab = (TabItem)mainWND.FindName("ProfileTab");
            ProfileTab.Visibility= Visibility.Collapsed;

            autWnd.Close();
            Application.Current.MainWindow = mainWND;
            mainWND.Show();
        }
        public static void ChangeLanguage(string lang = "eng")
        {
            //Application.ResourceAssembly
            //Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary langENG = Application.LoadComponent(new Uri(@"Resources/Dictionary3.xaml", UriKind.Relative)) as ResourceDictionary;
            ResourceDictionary langRU = Application.LoadComponent(new Uri(@"Resources/Dictionary1.xaml", UriKind.Relative)) as ResourceDictionary;
            switch (lang)
            {
                case "English":
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(langRU);
                        Application.Current.Resources.MergedDictionaries.Add(langENG);
                        break;
                    }
                case "Русский":
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(langENG);
                        Application.Current.Resources.MergedDictionaries.Add(langRU);
                        break;
                    }
            }
        }
    }
}
