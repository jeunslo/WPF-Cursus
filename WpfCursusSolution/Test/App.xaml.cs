using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Model.Foto model = new Model.Foto();
            ViewModel.Draggings vm = new ViewModel.Draggings(model);
            View.MainWindow view = new View.MainWindow();

            view.DataContext = vm;
            view.Show();
        }
    }
}
