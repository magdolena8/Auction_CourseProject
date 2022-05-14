using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace Lab_6
{
    internal partial class ViewModel
    {
        private Command _editCommand;
        private Command _startEditCommand;
        private Command _saveChangesCommnad;
        public Command EditCommand
        {
            get
            {
                return _editCommand ??
                    (_editCommand = new Command(obj =>
                    {
                        Product product = obj as Product;
                        try
                        {
                            //product.Title = Title.Text;
                            //product.Price = Price.Text;
                            //product.Description = Description.Text;
                            //product.Type = Type.Text;
                            //product.Image = photoPath;
                            //Serialization.ProductSerializer.Serialize(MainWindow, Serialization.ProductSerializer.XMLDataPath);           // Udate в бд
                        }
                        catch
                        {
                            //MessageBox.Show("Date error"); return;
                        }
                        Console.WriteLine("EditCMD log");
                    }));
            }
        }
        //public Command StartEditCommand
        //{
        //    get
        //    {
        //        return _startEditCommand ?? (_startEditCommand new Command(obj =>
        //        {

        //        }));
        //    }
        //}
    }
}
