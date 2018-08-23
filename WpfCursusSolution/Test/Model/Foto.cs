using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Test.Model
{
    public class Foto
    {
        public Foto()
        {
            List<BitmapImage> lijst = new List<BitmapImage>();
            lijst.Add(new BitmapImage(new Uri("pack://application:,,,/Images/bold.png", UriKind.Absolute)));
            lijst.Add(new BitmapImage(new Uri("pack://application:,,,/Images/italic.png", UriKind.Absolute)));
            lijst.Add(new BitmapImage(new Uri("pack://application:,,,/Images/bold.png", UriKind.Absolute)));
            lijst.Add(new BitmapImage(new Uri("pack://application:,,,/Images/italic.png", UriKind.Absolute)));
            FotoLijst = lijst;
            Image newFoto = new Image();
            /*newFoto.Source*/GridFoto = new BitmapImage(new Uri("pack://application:,,,/Images/bold.png", UriKind.Absolute));
            //GridFoto = newFoto;
        }
        public List<BitmapImage> FotoLijst { get; set; }

        public BitmapImage GridFoto { get; set; } 
    }
}
