using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace SearchFast
{
    [ValueConversion(typeof(Item), typeof(string))]
    public class FileSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            long length = (value as Item).Length;
            if (length == -1)
            {
                return "";
            }

            string[] units = { "б", "Кб", "Мб", "Гб", "Тб", "Пб" };
            double size = length;
            int unit = 0;
            while (size >= 1024)
            {
                size /= 1024;
                ++unit;
            }
            return String.Format("{0:0.#} {1}", size, units[unit]);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //not implemented for now
            throw new NotImplementedException();
        }
    }
}
