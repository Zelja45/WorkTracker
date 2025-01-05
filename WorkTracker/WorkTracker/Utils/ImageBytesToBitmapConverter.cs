using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WorkTracker.Utils
{
    public class ImageBytesToBitmapConverter:IValueConverter
    {
        public BitmapImage DefaultImage { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || value is byte[] imageBytes && imageBytes.Length == 0)
            {
                return DefaultImage;
            }
            if (value is byte[] bytes)
            {
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }
            }

            return DefaultImage; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
