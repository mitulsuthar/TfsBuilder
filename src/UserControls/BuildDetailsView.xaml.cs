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

namespace TfsBuilder.UserControls
{
    /// <summary>
    /// Interaction logic for BuildDetailsView.xaml
    /// </summary>
    public partial class BuildDetailsView : UserControl
    {
        public BuildDetailsView()
        {
            InitializeComponent();
        }
        //TODO: Use Command to open the drop location.
        private void btn_droplocation_Click(object sender, RoutedEventArgs e)
        {
            var myvalue = ((Button)sender).Tag;
            System.Diagnostics.Process.Start(myvalue.ToString());

        }
    }
}
