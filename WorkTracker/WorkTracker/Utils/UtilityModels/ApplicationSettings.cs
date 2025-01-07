using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
namespace WorkTracker.Utils.UtilityModels
{
    public class ApplicationSettings
    {
        public required string LanguageCode { get; set; }
        public required ITheme Theme { get; set; }
        public required string PrimaryColorCode { get; set; }
        public required string FontCode { get; set; }
    }
}
