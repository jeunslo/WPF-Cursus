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
using System.Windows.Shapes;

namespace WpfCursus
{
    /// <summary>
    /// Interaction logic for TekstVerwerken.xaml
    /// </summary>
    public partial class TekstVerwerken : Window
    {
        public TekstVerwerken()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            textBlockAanmelding.TextWrapping = TextWrapping.Wrap;
            textBlockAanmelding.Text = "Je probeerde aan te melden met: " + textBoxGebruikersnaam.Text + " en paswoord: " + psdBox.Password;
        }
    }
}
