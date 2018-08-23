using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCursus;

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
            IsEnablers();
        }


        private void Nieuw()
        { 
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;        
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
            IsEnablers();
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            int ifBedrag = bedrag + 1;
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * ifBedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
            IsEnablers();
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show("Nieuwe Bon?", "Nieuwe bon", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                this.Nieuw();
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = (Convert.ToDateTime(DatumBon.SelectedDate).ToShortDateString() + "om" + AankomstLabelTijd.Content.ToString()).Replace('/', '-').Replace(':', '-');
                dlg.DefaultExt = ".bon";
                dlg.Filter = ".bon bestanden | *.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bonBestand = new StreamWriter(dlg.FileName))
                    {
                        bonBestand.WriteLine(Convert.ToDateTime(DatumBon.SelectedDate).ToShortDateString());
                        bonBestand.WriteLine(AankomstLabelTijd.Content.ToString());
                        bonBestand.WriteLine(TeBetalenLabel.Content.ToString());
                        bonBestand.WriteLine(VertrekLabelTijd.Content.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("save mislukt: " + ex.Message);
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (CheckBedragGroterDanNul())
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.DefaultExt = ".bon";
                    dlg.Filter = ".bon bestanden | *.bon";
                    if (dlg.ShowDialog() == true)
                    {
                        using (StreamReader leesBon = new StreamReader(dlg.FileName))
                        {
                            DatumBon.SelectedDate = Convert.ToDateTime(leesBon.ReadLine());
                            AankomstLabelTijd.Content = Convert.ToDateTime(leesBon.ReadLine()).ToLongTimeString();
                            TeBetalenLabel.Content = leesBon.ReadLine();
                            VertrekLabelTijd.Content = Convert.ToDateTime(leesBon.ReadLine()).ToLongTimeString();
                        }
                        SourceStatus.Content = dlg.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("openen mislukt: " + ex.Message);
            }
        }

        private bool CheckBedragGroterDanNul()
        {
            if (Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", "")) > 0)
                return true;
            return false;
        }

        private void IsEnablers()
        {
            SaveBon.IsEnabled = CheckBedragGroterDanNul();
            PreviewBon.IsEnabled = CheckBedragGroterDanNul();
            SaveButton.IsEnabled = CheckBedragGroterDanNul();
            PreviewButton.IsEnabled = CheckBedragGroterDanNul();
        }

        private void PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Afdrukvoorbeeld preview = new Afdrukvoorbeeld();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }


        private double vertPositie;
        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(640, 320);
            PageContent inhoud = new PageContent();
            doc.Pages.Add(inhoud);
            FixedPage page = new FixedPage();
            inhoud.Child = page;
            page.Width = 640;
            page.Height = 320;
            vertPositie = 96;

            page.Children.Add(ImageRegel(logoImage.Source));
            page.Children.Add(Regel("datum: " + Convert.ToDateTime(DatumBon.SelectedDate).ToLongDateString()));
            page.Children.Add(Regel("starttijd: " + AankomstLabelTijd.Content.ToString()));
            page.Children.Add(Regel("eindtijd: " + VertrekLabelTijd.Content.ToString()));
            page.Children.Add(Regel("bedrag betaald: " + TeBetalenLabel.Content.ToString()));
            return doc;
        }

        private Image ImageRegel(ImageSource path)
        {
            Image deImage = new Image();
            deImage.Source = path;
            deImage.Margin = new Thickness(96, 96, 0, 0);
            return deImage;
        }

        private TextBlock Regel(string tekst)
        {
            double regelAfstand = 18;
            TextBlock deRegel = new TextBlock();
            deRegel.Margin = new Thickness(300, vertPositie, 0, 96);
            deRegel.FontSize = regelAfstand;
            deRegel.Text = tekst;
            vertPositie += regelAfstand * 2 ;
            return deRegel;
        }
    }
}
