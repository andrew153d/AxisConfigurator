using AxisConfigurator.ViewModels;
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

namespace AxisConfigurator.Views
{
    /// <summary>
    /// Interaction logic for SerialView.xaml
    /// </summary>
    public partial class SerialView : UserControl
    {
        private readonly SerialPortViewModel serialPortViewModel = new SerialPortViewModel();
        public SerialView()
        {
            InitializeComponent();
            DataContext = serialPortViewModel;
        }
    }
}
