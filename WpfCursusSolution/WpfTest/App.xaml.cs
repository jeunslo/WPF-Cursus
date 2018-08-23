using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.WenskaartGegevens model = new Model.WenskaartGegevens();
            ViewModel.WenskaartGegevensVM vm = new ViewModel.WenskaartGegevensVM(model);
            View.WenskaartWindow view = new View.WenskaartWindow();

            view.DataContext = vm;
            view.Show();
        }
    }

}
