using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WpfCursus
{
    public class Persoon
    {
        public Persoon(string naam, string telefoonnr, string groep, BitmapImage foto)
        {
            Naam = naam;
            Telefoonnr = telefoonnr;
            Foto = foto;
            if (groep == "Familie" || groep == "Werk" || groep == "Vrienden")
                Groep = groep;
            else
                throw new Exception("GroepInputFout");
        }
        public string Naam { get; set; }
        public string Telefoonnr { get; set; }
        public string Groep { get; set; }
        public BitmapImage Foto { get; set; }
    }
}
