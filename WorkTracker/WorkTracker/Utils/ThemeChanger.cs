using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WorkTracker.Utils
{
    public class ThemeChanger
    {
        private readonly Dictionary<string, Color> primaryColors;

        public ThemeChanger()
        {
            string primaryColor1= ConfigurationManager.AppSettings["PrimaryColor1"];
            string primaryColor2 = ConfigurationManager.AppSettings["PrimaryColor2"];
            string primaryColor3 = ConfigurationManager.AppSettings["PrimaryColor3"];
            primaryColors = new Dictionary<string, Color>()
            {
                { primaryColor1, (Color)ColorConverter.ConvertFromString(primaryColor1) },
                { primaryColor2,(Color)ColorConverter.ConvertFromString(primaryColor2) },
                { primaryColor3,(Color)ColorConverter.ConvertFromString(primaryColor3) },
            };
        }
        public void ApplyTheme(ITheme theme)
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(theme);
        }
        public ITheme SetPrimaryColor(string primaryColorKey)
        {
           Color newPrimaryColor= primaryColors[primaryColorKey];
           PaletteHelper paletteHelper = new PaletteHelper();
           var currentTheme = paletteHelper.GetTheme();
           currentTheme.SetPrimaryColor(newPrimaryColor);
           paletteHelper.SetTheme(currentTheme);
           return currentTheme;
        }
        public ITheme SwitchUIBaseTheme()
        {
            PaletteHelper paletteHelper = new PaletteHelper();
            ITheme currentTheme=paletteHelper.GetTheme();
            if (currentTheme.GetBaseTheme() == BaseTheme.Dark)
            {
                currentTheme.SetBaseTheme(Theme.Light);
            }
            else
            {
                currentTheme.SetBaseTheme(Theme.Dark);
            }
            paletteHelper.SetTheme(currentTheme);
            return currentTheme;
        }
    }
}
