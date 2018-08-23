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

namespace WpfCursus
{
    /// <summary>
    /// Interaction logic for Verkeerslicht.xaml
    /// </summary>
    public partial class Verkeerslicht : Window
    {
        public Verkeerslicht()
        {
            InitializeComponent();
        }

        private void OpgeletKnop_Click(object sender, RoutedEventArgs e)
        {
            Button knop = (Button)sender;
            knop.IsEnabled = false;
            if(Roodlicht.Opacity == 1)
                GoKnop.IsEnabled = true;
            if (Groenlicht.Opacity == 1)
                StopKnop.IsEnabled = true;
            Roodlicht.Opacity = 0;
            Groenlicht.Opacity = 0;
            Orangjelicht.Opacity = 1;
        }

        private void StopGoKnop_Click(object sender, RoutedEventArgs e)
        {
            Button knop = (Button)sender;
            knop.IsEnabled = false;
            OpgeletKnop.IsEnabled = true;
            Orangjelicht.Opacity = 0;
            if (knop.Tag.ToString() == "Red")
                Roodlicht.Opacity = 1;
            if (knop.Tag.ToString() == "Green")
                Groenlicht.Opacity = 1;             
        }
    }
}
