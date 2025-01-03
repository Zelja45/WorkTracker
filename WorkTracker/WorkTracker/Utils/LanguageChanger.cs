using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkTracker.Utils
{
    public class LanguageChanger
    {
        private ResourceDictionary _resourceDictionary;
        public LanguageChanger(ResourceDictionary resourceDictionary)
        {
            _resourceDictionary = resourceDictionary;
        }
        public void ApplyLanguage(string languageCode)
        {
            Thread.CurrentThread.CurrentCulture=new System.Globalization.CultureInfo(languageCode);
            Thread.CurrentThread.CurrentUICulture=new System.Globalization.CultureInfo(languageCode);

            var existingLanguageDictionary = _resourceDictionary.MergedDictionaries
                .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Language-"));

            ResourceDictionary resource = new ResourceDictionary()
            {
                Source = new Uri($"/Properties/Languages/Language-{languageCode}.xaml", UriKind.Relative)
            };

            _resourceDictionary.MergedDictionaries.Add(resource);
        }
    }
}
