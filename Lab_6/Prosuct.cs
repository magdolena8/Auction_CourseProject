using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    [Serializable]
    public class Product
    {
        public string Title { get; set; }
        public string Price { get; set; }
        public DateTime TimeLeft { get; set; }
        public int Bids { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Image { get; set; } = null;

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
    }
}
