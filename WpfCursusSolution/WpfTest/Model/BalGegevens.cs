using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfTest.Model
{
    public class Bal
    {
        public Bal()
        {
        }

        public Bal(SolidColorBrush balKleur, double xPositie, double yPositie)
        {
            BalKleur = balKleur;
            XPositie = xPositie;
            YPositie = yPositie;
        }
        public SolidColorBrush BalKleur { get; set; }
        public double XPositie { get; set; }
        public double YPositie { get; set; }
        public int Tag { get; set; }
    }
}
