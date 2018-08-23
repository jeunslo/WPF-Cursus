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
    /// Interaction logic for PizzaBestellenWindow.xaml
    /// </summary>
    public partial class PizzaBestellenWindow : Window
    {
        public PizzaBestellenWindow()
        {
            InitializeComponent();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder tekst = new StringBuilder();
            tekst.Append("U heeft " + hoeveelheidLabel.Content.ToString() + " ");
            foreach (var keuze in grootteKeuze.Children)
            {
                if (keuze is RadioButton)
                {
                    var radiobutton = (RadioButton)keuze;
                    if (radiobutton.IsChecked == true)
                        tekst.Append(radiobutton.Content.ToString() + " ");
                }
            }
            tekst.Append("pizza('s) besteld met: ");
            List<string> ingredientenList = new List<string>(); 
            foreach (var keuze in Ingredienten.Children)
            {
                if (keuze is CheckBox)
                {
                    var checkbox = (CheckBox)keuze;
                    if (checkbox.IsChecked == true)
                        ingredientenList.Add(checkbox.Content.ToString());
                }
            }
            var lastItem = ingredientenList[ingredientenList.Count - 1];
            ingredientenList.Remove(ingredientenList[ingredientenList.Count-1]);
            tekst.Append(string.Join(", ", ingredientenList));
            tekst.Append(" en " + lastItem + Environment.NewLine);
            if (toggleKorst.IsChecked == true)
                tekst.Append("met een " + toggleKorst.Content.ToString());
            if (toggleKaas.IsChecked == true)
            {
                if (toggleKorst.IsChecked == true)
                    tekst.Append(" en ");
                tekst.Append("overstrooid met " + toggleKaas.Content.ToString());
            }
            opsommingsLabel.Content = tekst;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var hoeveelheid = Convert.ToInt16(hoeveelheidLabel.Content.ToString());
            if (hoeveelheid < 10)
                hoeveelheid++;
            hoeveelheidLabel.Content = hoeveelheid.ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var hoeveelheid = Convert.ToInt16(hoeveelheidLabel.Content.ToString());
            if (hoeveelheid > 1)
                hoeveelheid--;
            hoeveelheidLabel.Content = hoeveelheid.ToString();
        }
    }
}
