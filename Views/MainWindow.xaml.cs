using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using AxisConfigurator.Views;
using AxisConfigurator.ViewModels;
namespace AxisConfigurator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        AxisConnection axisComms = new AxisConnection();
        public MainWindow()
        {
            InitializeComponent();
            axisComms.PortConnectedAction += OnPortConnectionUpdate;
        }

        private void MyTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Ensure the event handler is not triggered when the control is being initialized
            if (e.AddedItems.Count > 0)
            {
                // Get the selected TabItem
                TabItem selectedTab = (TabItem)MainTabControl.SelectedItem;

                // Call functions based on the selected tab
                if (selectedTab == Status)
                {
                    //OnTab1Selected();
                }
                else if (selectedTab == LED)
                {
                    //ledView.OnLedTabSelected();
                }
                else if (selectedTab == TabItem)
                {
                    //OnTab3Selected();
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (axisComms.isConnected)
            {
                axisComms.Disconnect();
            }
            else
            {
                axisComms.Connect();
            }
        }

        public void OnPortConnectionUpdate(bool connected)
        {
            if(connected)
            {
                ConnectButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
                ConnectButton.Content = "Connected";
            }
            else
            {
                ConnectButton.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                ConnectButton.Content = "Disconnected";
            }
        }     
    }
}
