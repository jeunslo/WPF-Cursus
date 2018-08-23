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
using System.Windows.Data;

namespace ParkingBonMVVM.ViewModel
{
    public class ParkingVM : ViewModelBase
    {
        private Model.Parking parking;

        public ParkingVM(Model.Parking initParking)
        {
            parking = initParking;
        }

        public string AankomstTijd
        {
            get { return parking.AankomstTijd; }
            set
            {
                parking.AankomstTijd = value;
                RaisePropertyChanged("AankomstTijd");
            }
        }

        public string VertrekTijd
        {
            get { return parking.VertrekTijd; }
            set
            {
                parking.VertrekTijd = value;
                RaisePropertyChanged("VertrekTijd");
            }
        }

        public DateTime Datum
        {
            get { return parking.Datum; }
            set
            {
                parking.Datum = value;
                RaisePropertyChanged("Datum");
            }
        }

        public decimal Bedrag
        {
            get { return parking.Bedrag; }
            set
            {
                parking.Bedrag = value;
                RaisePropertyChanged("Bedrag");
            }
        }

        public RelayCommand<RoutedEventArgs> NewCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Nieuw); }
        }

        private void Nieuw(RoutedEventArgs e)
        {
            Datum = DateTime.Now;
            AankomstTijd = DateTime.Now.ToLongTimeString();
            VertrekTijd = DateTime.Now.ToLongTimeString();
            Bedrag = 0m;
        }

        public RelayCommand<RoutedEventArgs> OpslaanCommand
        {
            get { return new RelayCommand<RoutedEventArgs>(Opslaan); }
        }

        private void Opslaan(RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = string.Format("{0:dd-MM-yyyy}om{1:H-mm}", Datum.ToShortDateString().Replace("/","-"), Convert.ToDateTime(AankomstTijd).ToShortTimeString().Replace(":", "-"));
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Bonnen |*.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(Datum.ToShortDateString());
                        bestand.WriteLine(AankomstTijd);
                        bestand.WriteLine(Bedrag.ToString());
                        bestand.WriteLine(VertrekTijd);
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
                dlg.DefaultExt = ".bon";
                dlg.Filter = "Bonnen |*.bon";
                if (dlg.ShowDialog() == true)
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        Datum = Convert.ToDateTime(bestand.ReadLine());
                        AankomstTijd = bestand.ReadLine();
                        decimal.TryParse(bestand.ReadLine(), out decimal bedrag);
                        Bedrag = bedrag;
                        VertrekTijd = bestand.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Openen Mislukt", ex.Message, MessageBoxButton.OK);
            }
        }

        public RelayCommand AfsluitenCommand
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
            if (MessageBox.Show("Afsluiten", "Wilt u het programma afsluiten?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        public RelayCommand MinCommand
        {
            get { return new RelayCommand(MinButton); }
        }

        private void MinButton()
        {
            if (Bedrag != 0)
            {
                Bedrag--;
                VertrekTijd = Convert.ToDateTime(VertrekTijd).AddMinutes(-30).ToLongTimeString();
            }
        }

        public RelayCommand PlusCommand
        {
            get { return new RelayCommand(PlusButton); }
        }

        private void PlusButton()
        {
            if (Convert.ToDateTime(VertrekTijd).AddMinutes(30).Hour < 22)
            {
                Bedrag++;
                VertrekTijd = Convert.ToDateTime(VertrekTijd).AddMinutes(30).ToLongTimeString();
            }
        }
    }
}
