using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Lab_6.View.Converters
{
    public class DateTimeToTimespanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime? dateTime = value as DateTime?;
            string formatted = null;
            if (dateTime != null)
            {
                DateTime r = (DateTime)dateTime;
                TimeSpan res = ((DateTime)dateTime).Subtract(DateTime.Now);

                if (dateTime < DateTime.Now)
                {
                    return "SOLD";
                }
                if (res.Duration().Days > 0)
                {
                    formatted = String.Format("{0}{1}", (String.Format("{0:0} day{1}, ", res.Days, res.Days == 1 ? string.Empty : "s")), (String.Format("{0:0} hour{1} ", res.Hours, res.Hours == 1 ? string.Empty : "s")));
                }
                else if (res.Duration().Hours > 0)
                {
                    formatted = String.Format("{0}{1}", (String.Format("{0:0} day{1}, ", res.Hours, res.Hours == 1 ? string.Empty : "s")), (String.Format("{0:0} hour{1} ", res.Minutes, res.Minutes == 1 ? string.Empty : "s")));
                }
                else if (res.Duration().Minutes > 0)
                {
                    formatted = String.Format("{0}{1}", (String.Format("{0:0} day{1}, ", res.Minutes, res.Minutes == 1 ? string.Empty : "s")), (String.Format("{0:0} hour{1} ", res.Seconds, res.Seconds == 1 ? string.Empty : "s")));
                }



                //return res.ToString(@"hh\:mm");
                return formatted;

            }
            //if (parameter != null && parameter.ToString() == "EN")
            //return ((DateTime)value).ToString("MM-dd-yyyy");


            return ((TimeSpan)value).ToString("0:%d");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
