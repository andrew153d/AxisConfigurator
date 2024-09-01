using AxisConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AxisConfigurator.ViewModels
{
    public class SerialPortViewModel : INotifyPropertyChanged
    {
        private readonly SerialPortModel _serialPortModel;
        public ICommand AutoSearchCommand { get; private set; }
        public SerialPortViewModel()
        {
            _serialPortModel = SerialPortModel.Instance;
            ConnectCommand = new RelayCommand(PortControlCommand);
            AutoSearchCommand = new RelayCommand(StartAutoSearch);
        }
        public ObservableCollection<string> PortList
        {
            get { return _serialPortModel.PortList; }
            set
            {
                _serialPortModel.PortList = value;
                OnPropertyChanged(nameof(_serialPortModel.PortList));
            }
        }

        public bool PortConnected => _serialPortModel.activeSerialPort.IsOpen;

        private string _selectedPort;
        public string SelectedPort
        {
            get => _selectedPort;
            set
            {
                _selectedPort = value;
                OnPropertyChanged(nameof(SelectedPort));
            }
        }

        public ICommand ConnectCommand { get; }
        private void StartAutoSearch()
        {
            _serialPortModel.StartAutoSearch();
        }
        private void PortControlCommand()
        {
            if (PortConnected)
            {
                _serialPortModel.Close();
            }
            else
            {
                _serialPortModel.Open(_selectedPort);
            }
            OnPropertyChanged(nameof(PortConnected));

        }

        private void ClosePort()
        {
            //_serialPortModel.Close();
        }

        private void SendData(string data)
        {
            //_serialPortModel.Write(data);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
