using Microsoft.Win32;
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

namespace FontFamilyFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _fullFontPath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FilePickClicked(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog();

            if ((bool)ofd.ShowDialog())
            {
                _fullFontPath = ofd.FileName;
                FontFileName.Text = System.IO.Path.GetFileName(_fullFontPath);

                try
                {
                    var familynames = Fonts.GetFontFamilies(_fullFontPath).ToList();

                    var wssnames =
                        (from familyname in familynames from f in familyname.FamilyNames select f.Value).ToList();

                    FontFamilies.ItemsSource = wssnames;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something happened :( - " + ex.Message);
                }
            }
        }

        private void FontFamilyNameSelected(object sender, SelectionChangedEventArgs e)
        {
            Clipboard.SetText((string) FontFamilies.SelectedValue);
            ClipboardMessage.Content = string.Format("'{0}' copied to clipboard", (string) FontFamilies.SelectedValue);
        }
    }
}
