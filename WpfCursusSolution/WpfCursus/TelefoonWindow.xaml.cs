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
using System.ComponentModel;
using System.Media;

namespace WpfCursus
{
    /// <summary>
    /// Interaction logic for TelefoonWindow.xaml
    /// </summary>
    public partial class TelefoonWindow : Window
    {
        public TelefoonWindow()
        {
            InitializeComponent();
        }

        public List<Persoon> personen = new List<Persoon>();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            personen.Add(new Persoon("Anne", "111", "Vrienden", new BitmapImage(new Uri("Images/Telefoon/anne.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Bob", "222", "Vrienden", new BitmapImage(new Uri("Images/Telefoon/bob.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Collega1", "333", "Werk", new BitmapImage(new Uri("Images/Telefoon/collega1.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Collega2", "444", "Werk", new BitmapImage(new Uri("Images/Telefoon/collega2.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Collega3", "555", "Werk", new BitmapImage(new Uri("Images/Telefoon/collega3.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Ed", "666", "Vrienden", new BitmapImage(new Uri("Images/Telefoon/ed.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Grote zus", "777", "Familie", new BitmapImage(new Uri("Images/Telefoon/grotezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Kleine zus", "/", "Familie", new BitmapImage(new Uri("Images/Telefoon/kleinezus.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Tante non", "888", "Familie", new BitmapImage(new Uri("Images/Telefoon/tantenon.jpg", UriKind.Relative))));
            personen.Add(new Persoon("Vader", "999", "Familie", new BitmapImage(new Uri("Images/Telefoon/vader.jpg", UriKind.Relative))));

            ComboBoxPersoon.Items.Add("- Alle groepen -");
            ComboBoxPersoon.Items.Add("Vrienden");
            ComboBoxPersoon.Items.Add("Werk");
            ComboBoxPersoon.Items.Add("Familie");
            ComboBoxPersoon.SelectedIndex = 0;
        }

        private void ComboBoxPersoon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxPersoon.Items.Clear();
            foreach (Persoon persoon in personen)
            {
                if (persoon.Groep == ComboBoxPersoon.SelectedItem.ToString() || ComboBoxPersoon.SelectedIndex == 0)
                    ListBoxPersoon.Items.Add(persoon);
            }
            ListBoxPersoon.Items.SortDescriptions.Add(new SortDescription("Naam", ListSortDirection.Ascending));
        }

        private void TelefoonButton_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPersoon.SelectedItem != null)
            {
                Persoon item = (Persoon)ListBoxPersoon.SelectedItem;
                string tekst = "Wil je " + item.Naam + " bellen \n op het nummer: " + item.Telefoonnr;
                if (MessageBox.Show(tekst, "Telefoon", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
                {
                    SoundPlayer speler = new SoundPlayer("PHONE.wav");
                    if (speler != null)
                        speler.Play();
                    else
                        throw new Exception();
                }
            }
            else
                MessageBox.Show("Je moet eerst iemand selecteren", "Niemand gekozen", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
