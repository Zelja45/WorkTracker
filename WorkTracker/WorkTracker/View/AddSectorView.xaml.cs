using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkTracker.View
{
    /// <summary>
    /// Interaction logic for AddSectorView.xaml
    /// </summary>
    public partial class AddSectorView : UserControl
    {
        public AddSectorView()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void TextBox_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            string fullText = textBox.Text.Insert(textBox.SelectionStart, e.Text);
            var regex = new System.Text.RegularExpressions.Regex(@"^\d*(?:[.,]?\d{0,})?$");
            e.Handled = !regex.IsMatch(fullText);
        }
    }
}
