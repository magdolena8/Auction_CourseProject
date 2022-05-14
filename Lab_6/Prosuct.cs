using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Lab_6
{
    [Serializable]
    public class Product : INotifyPropertyChanged
    {
        
        private string _title;
        private string _price;
        private DateTime _timeLeft;
        private int _bids;
        private string _description;
        private string _type;
        private string _image;
        [Key]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("Price");
            }
        }
        public DateTime TimeLeft
        {
            get { return _timeLeft; }
            set
            {
                _timeLeft = value;
                OnPropertyChanged("TimeLeft");
            }
        }
        public int Bids
        {
            get { return _bids; }
            set
            {
                _bids = value;
                OnPropertyChanged("Bids");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }
        public string Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Product(string _title, string _price, DateTime _timeleft, int _bids, string _description, string _type, string _path)
        {
            Title = _title;
            Price = _price;
            TimeLeft = _timeleft;
            Bids = _bids;
            Description = _description;
            Type = _type;
            Image = _path;
        }
        public Product()
        {
            Title = null;
            Price = null ;
            TimeLeft = DateTime.MinValue;
            Bids = 0;
            Description = null;
            Type = null;
            Image = null;
        }
    }
}
