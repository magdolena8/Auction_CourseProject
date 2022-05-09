using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Lab_6.Serialization
{
    public static class ProductSerializer
    {
        public static string XMLDataPath = @"D:\Stud\OOP\4_sem\Lab_6\Lab_6\Data\Data.xml";
        public static void Serialize(List<Product> prdocutList, string path)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<Product>));
            using (FileStream fs = new FileStream(path, FileMode.Truncate))
            {
                xmlSerial.Serialize(fs, prdocutList);
            }
        }
        public static void Deserialize(string path, List<Product> prdocutList)
        {
            XmlSerializer xmlSerial = new XmlSerializer(typeof(List<Product>));
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    var tempProductList = xmlSerial.Deserialize(fs) as List<Product>;
                    foreach (Product i in tempProductList)
                    {
                        prdocutList.Add(i);
                    }
                }
            }
        }
    }
}
