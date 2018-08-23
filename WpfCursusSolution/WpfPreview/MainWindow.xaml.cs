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

namespace WpfPreview
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void PrintExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            PreveiwWindow preview = new PreveiwWindow();
            preview.Owner = this;
            preview.AfdrukDocument = StelAfdrukSamen();
            preview.ShowDialog();
        }

        //***************Methods*******************************************
        private double A4breedte = 500;
        private double A4hoogte = 500;

        private FixedDocument StelAfdrukSamen()
        {
            FixedDocument doc = new FixedDocument();
            doc.DocumentPaginator.PageSize = new System.Windows.Size(A4breedte, A4hoogte);
            PageContent inhoud = new PageContent();
            doc.Pages.Add(inhoud);
            FixedPage page = new FixedPage();
            inhoud.Child = page;
            page.Width = A4breedte;
            page.Height = A4hoogte;

            page.Children.Add(MaakCanvas());
            page.Children.Add(MaakTextBlock());
            return doc;
        }

        private Canvas MaakCanvas()
        {
            Canvas newCanvas = new Canvas();
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/kerstkaart.jpg", UriKind.Absolute));
            newCanvas.Background = ib;
            //foreach (Model.Bal bal in BalLijst)
            //{
            //    Ellipse newBal = new Ellipse();
            //    newBal.Fill = bal.BalKleur;
            //    Canvas.SetLeft(newBal, bal.XPositie);
            //    Canvas.SetRight(newBal, bal.YPositie);
            //    newCanvas.Children.Add(newBal);
            //}
            newCanvas.Height = 400;
            newCanvas.Width = A4breedte;
            return newCanvas;
        }

        private TextBlock MaakTextBlock()
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Text = "WensTekst";
            newTextBlock.FontSize = 20;
            newTextBlock.FontFamily = new FontFamily("Arial");
            return newTextBlock;
        }
    }
}
