using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfTest.Model
{
    public class WenskaartGegevens
    {
        public WenskaartGegevens()
        {
            List<Kleur> kleurLijst = new List<Kleur>();
            foreach (PropertyInfo info in typeof(Colors).GetProperties())
            {
                SolidColorBrush deKleur = (SolidColorBrush)new BrushConverter().ConvertFromString(info.Name);
                Kleur kleurke = new Kleur();
                kleurke.Borstel = deKleur;
                kleurke.Naam = info.Name;
                kleurke.Hex = deKleur.ToString();
                kleurke.Rood = deKleur.Color.R;
                kleurke.Groen = deKleur.Color.G;
                kleurke.Blauw = deKleur.Color.B;
                kleurLijst.Add(kleurke);
            }
            AlleKleuren = kleurLijst;
            Lettergrootte = 10;
            WensTekst = "";
            LetterType = new FontFamily("Arial");
            BestandsLocatie = "nieuw";
            SelectedIndex = 137;
        }
        public string BackgroundName { get; set; }
        public BitmapImage BackgroundImage { get; set; }
        public List<Bal> BalLijst { get; set; }
        public string WensTekst { get; set; }
        public FontFamily LetterType { get; set; }
        public int Lettergrootte { get; set; }
        public List<Kleur> AlleKleuren { get; set; }
        public int SelectedIndex { get; set; }
        public string BestandsLocatie { get; set; }
        public IDocumentPaginatorSource AfdrukDocument { get; set; }
    }
}
