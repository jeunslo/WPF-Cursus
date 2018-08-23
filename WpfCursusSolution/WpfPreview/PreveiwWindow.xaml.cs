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
using System.Windows.Shapes;

namespace WpfPreview
{
    /// <summary>
    /// Interaction logic for PreveiwWindow.xaml
    /// </summary>
    public partial class PreveiwWindow : Window
    {
        public PreveiwWindow()
        {
            InitializeComponent();
        }
        public IDocumentPaginatorSource AfdrukDocument
        {
            get
            {
                return printpreview.Document;
            }
            set
            {
                printpreview.Document = value;
            }
        }
    }
}
