using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingBonMVVM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.Parking parkingModel = new Model.Parking();
            ViewModel.ParkingVM vm = new ViewModel.ParkingVM(parkingModel);
            View.ParkingBonView parkingView = new View.ParkingBonView();

            parkingView.DataContext = vm;
            parkingView.Show();
        }
    }
}
