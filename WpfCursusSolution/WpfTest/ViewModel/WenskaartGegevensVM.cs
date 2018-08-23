using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
using WpfTest.View;

namespace WpfTest.ViewModel
{
    public class WenskaartGegevensVM : ViewModelBase
    {
        private Model.WenskaartGegevens wenskaartModel;
        public WenskaartGegevensVM(Model.WenskaartGegevens nieuwWenskaartModel)
        {
            wenskaartModel = nieuwWenskaartModel;
        }

        public string BackgroundName
        {
            get { return wenskaartModel.BackgroundName; }
            set
            {
                wenskaartModel.BackgroundName = value;
                RaisePropertyChanged("BackgroundName");
            }
        }

        public BitmapImage BackgroundImage
        {
            get { return wenskaartModel.BackgroundImage; }
            set
            {
                wenskaartModel.BackgroundImage = value;
                RaisePropertyChanged("BackgroundImage");
            }
        }

        private List<Model.Bal> ballenlijst = new List<Model.Bal>();
        public List<Model.Bal> BalLijst
        {
            get { return wenskaartModel.BalLijst; }
            set
            {
                wenskaartModel.BalLijst = value;
                RaisePropertyChanged("BalLijst");
            }
        }

        public string WensTekst
        {
            get { return wenskaartModel.WensTekst; }
            set
            {
                wenskaartModel.WensTekst = value;
                RaisePropertyChanged("WensTekst");

            }
        }

        public FontFamily LetterType
        {
            get { return wenskaartModel.LetterType; }
            set
            {
                wenskaartModel.LetterType = value;
                RaisePropertyChanged("LetterType");
            }
        }

        public int Lettergrootte
        {
            get { return wenskaartModel.Lettergrootte; }
            set
            {
                wenskaartModel.Lettergrootte = value;
                RaisePropertyChanged("Lettergrootte");
            }
        }

        public List<Model.Kleur> AlleKleuren
        {
            get { return wenskaartModel.AlleKleuren; }
            set
            {
                wenskaartModel.AlleKleuren = value;
                RaisePropertyChanged("AlleKleuren");
            }
        }

        public int SelectedIndex
        {
            get { return wenskaartModel.SelectedIndex; }
            set
            {
                wenskaartModel.SelectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        public string BestandsLocatie
        {
            get { return wenskaartModel.BestandsLocatie; }
            set
            {
                wenskaartModel.BestandsLocatie = value;
                RaisePropertyChanged("BestandsLocatie");
            }
        }

        public IDocumentPaginatorSource AfdrukDocument
        {
            get { return wenskaartModel.AfdrukDocument; }
            set
            {
                wenskaartModel.AfdrukDocument = value;
                RaisePropertyChanged("AfdrukDocument");
            }
        }

        //****************************************Alle Commands*****************************************

        public RelayCommand<RoutedEventArgs> KerstImageCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(KerstImageClick); }
        }

        private void KerstImageClick(RoutedEventArgs e)
        {
            Nieuw(e);
            BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Images/kerstkaart.jpg", UriKind.Absolute));
            BackgroundName = "KerstKaart";
        }

        public RelayCommand<RoutedEventArgs> GeboorteImageCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(GeboorteImageClick); }
        }

        private void GeboorteImageClick(RoutedEventArgs e)
        {
            Nieuw(e);
            BackgroundImage = new BitmapImage(new Uri("pack://application:,,,/Images/geboortekaart.jpg", UriKind.Absolute));
            BackgroundName = "Geboortekaart";
        }

        public RelayCommand MinCommand
        {
            get { return new RelayCommand(MinButton); }
        }

        private void MinButton()
        {
            if (Lettergrootte > 10)
                Lettergrootte--;
        }

        public RelayCommand PlusCommand
        {
            get { return new RelayCommand(PlusButton); }
        }

        private void PlusButton()
        {
            if (Lettergrootte < 40)
                Lettergrootte++;
        }

        //*********************************DragDropCommands*********************************************
        private Ellipse FindEllipse(Object sleepitem)
        {
            DependencyObject keuze = (DependencyObject)sleepitem;
            while (keuze != null)
            {
                if (keuze is Ellipse)
                    return (Ellipse)keuze;
                keuze = VisualTreeHelper.GetParent(keuze);
            }
            return null;
        }

        public RelayCommand<MouseEventArgs> DragBalCommand
        {
            get { return new RelayCommand<MouseEventArgs>(OnMouseMove); }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            Ellipse dragBal = FindEllipse(e.OriginalSource);
            if (e.LeftButton == MouseButtonState.Pressed && dragBal != null)
            {
                DataObject dataObjDragBal = new DataObject("Source", dragBal);
                DragDrop.DoDragDrop(dragBal, dataObjDragBal, DragDropEffects.Move);
            }
        }

        public RelayCommand<DragEventArgs> DropBalCommand
        {
            get { return new RelayCommand<DragEventArgs>(OnDrop); }
        }

        public static int tag = 0;
        public void OnDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Source"))
            {
                if (e.OriginalSource is Canvas)
                {
                    Canvas dropCanvas = (Canvas)e.OriginalSource;
                    var muisPos = e.GetPosition(dropCanvas);
                    Ellipse dragBal = (Ellipse)e.Data.GetData("Source");
                    int dragBalTag = 0;
                    if (dragBal.Tag != null)
                        dragBalTag = (int)dragBal.Tag;
                    else
                        dragBalTag = -1;
                    Model.Bal bal = new Model.Bal();
                    bal.BalKleur = (SolidColorBrush)dragBal.Fill;
                    bal.XPositie = muisPos.X - 20;
                    bal.YPositie = muisPos.Y - 20;
                    Model.Bal bestaandBal = ballenlijst.Find(x => x.Tag == dragBalTag);
                    if (bestaandBal != null)
                    {
                        bal.Tag = dragBalTag;
                        ballenlijst.Remove(bestaandBal);
                    }
                    else
                        bal.Tag = tag++;
                    ballenlijst.Add(bal);
                    BalLijst = null;
                    BalLijst = ballenlijst;
                }
            }
        }

        public RelayCommand<DragEventArgs> RemoveBalCommand
        {
            get { return new RelayCommand<DragEventArgs>(RemoveOnDrop); }
        }

        public void RemoveOnDrop(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("Source"))
            {
                Image dropImage = (Image)e.OriginalSource;
                Ellipse dragBal = (Ellipse)e.Data.GetData("Source");
                int dragBalTag = 0;
                if (dragBal.Tag != null)
                    dragBalTag = (int)dragBal.Tag;
                Model.Bal bestaandBal = ballenlijst.Find(x => x.Tag == dragBalTag);
                if (bestaandBal != null)
                {
                    ballenlijst.Remove(bestaandBal);
                }
                BalLijst = null;
                BalLijst = ballenlijst;
            }
        }
        //********************CommonDialog command***********************************************
        public RelayCommand<RoutedEventArgs> NewCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Nieuw); }
        }

        private void Nieuw(RoutedEventArgs e)
        {
            BackgroundImage = null;
            Lettergrootte = 10;
            WensTekst = "";
            LetterType = new FontFamily("Arial");
            BalLijst = ballenlijst;
            if (BalLijst != null)
            {
                BalLijst = null;
                ballenlijst.Clear();
            }
            SelectedIndex = 137;
            BestandsLocatie = "nieuw";
        }

        public RelayCommand<RoutedEventArgs> SaveCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Opslaan); }
        }

        private void Opslaan(RoutedEventArgs e)
        {
            try
            {
                if (BackgroundImage != null)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.FileName = "";
                    dlg.DefaultExt = ".txt";
                    dlg.Filter = "Kaarten |*.txt";
                    if (dlg.ShowDialog() == true)
                    {
                        using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                        {
                            bestand.WriteLine(BackgroundName);
                            bestand.WriteLine(BackgroundImage.ToString());
                            bestand.WriteLine(BalLijst.Count);
                            if (BalLijst != null)
                            {
                                foreach (Model.Bal bal in BalLijst)
                                {
                                    bestand.WriteLine(bal.BalKleur.ToString());
                                    bestand.WriteLine(bal.XPositie);
                                    bestand.WriteLine(bal.YPositie);
                                }
                            }
                            bestand.WriteLine(WensTekst);
                            bestand.WriteLine(LetterType);
                            bestand.WriteLine(Lettergrootte);
                        }
                        BestandsLocatie = dlg.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save Mislukt", ex.Message, MessageBoxButton.OK);
            }
        }

        public RelayCommand<RoutedEventArgs> OpenCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Openen); }
        }

        private void Openen(RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.FileName = "";
                dlg.DefaultExt = ".txt";
                dlg.Filter = "Kaarten |*.txt";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        Nieuw(e);
                        BackgroundName = bestand.ReadLine();
                        BackgroundImage = new BitmapImage(new Uri(bestand.ReadLine(), UriKind.Absolute));
                        int.TryParse(bestand.ReadLine(), out int aantal);
                        tag = 0;
                        for (int i = 0; i < aantal; i++)
                        {
                            Model.Bal bal = new Model.Bal();
                            string sKleur = bestand.ReadLine();
                            SolidColorBrush deKleur = (SolidColorBrush)new BrushConverter().ConvertFromString(sKleur);
                            bal.BalKleur = deKleur;
                            double.TryParse(bestand.ReadLine(), out double xPos);
                            bal.XPositie = xPos;
                            double.TryParse(bestand.ReadLine(), out double yPos);
                            bal.YPositie = yPos;
                            bal.Tag = tag++;
                            ballenlijst.Add(bal);
                        }
                        BalLijst = ballenlijst;
                        WensTekst = bestand.ReadLine();
                        LetterType = new FontFamily(bestand.ReadLine());
                        int.TryParse(bestand.ReadLine(), out int grootte);
                        Lettergrootte = grootte;
                    }
                    BestandsLocatie = dlg.FileName;
                }
            }
            catch (Exception ex)
            {
                Nieuw(e);
                MessageBox.Show("Openen Mislukt", ex.Message, MessageBoxButton.OK);
            }
        }

        public RelayCommand PreviewCommand
        {
            get { return new RelayCommand(PrintPreviewen); }
        }

        public void PrintPreviewen()
        {
            if (BackgroundImage != null)
            {
                PrintPreviewWindow preview = new PrintPreviewWindow();
                AfdrukDocument = StelAfdrukSamen();
                preview.DataContext = this;
                preview.Show();
            }
        }

        public RelayCommand CloseCommand
        {
            get { return new RelayCommand(Afsluiten); }
        }

        private void Afsluiten()
        {
            Application.Current.MainWindow.Close();
        }

        public RelayCommand<CancelEventArgs> ClosingCommand
        {
            get { return new RelayCommand<CancelEventArgs>(CancelApp); }
        }

        private void CancelApp(CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u het programma sluiten?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        //***************Methods(voor printpreview)*******************************************
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
            ib.ImageSource = BackgroundImage;
            newCanvas.Background = ib;
            newCanvas.Height = 400;
            newCanvas.Width = 500;
            foreach (Model.Bal bal in BalLijst)
            {
                Ellipse newBal = new Ellipse();
                newBal.Fill = bal.BalKleur;
                Canvas.SetLeft(newBal, bal.XPositie);
                Canvas.SetTop(newBal, bal.YPositie);
                newCanvas.Children.Add(newBal);
            }
            newCanvas.Width = A4breedte;
            return newCanvas;
        }

        private TextBlock MaakTextBlock()
        {
            TextBlock newTextBlock = new TextBlock();
            newTextBlock.Width = 500;
            newTextBlock.Height = 100;
            newTextBlock.Margin = new Thickness(0, 400, 0, 0);
            newTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
            newTextBlock.TextAlignment = TextAlignment.Center;
            newTextBlock.FontSize = Lettergrootte;
            newTextBlock.FontFamily = LetterType;
            newTextBlock.Text = WensTekst;
            return newTextBlock;
        }
    }
}
