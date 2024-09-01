using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AxisConfigurator.Models
{
    public class SerialPortModel : INotifyPropertyChanged
    {
        #region Singleton
        private static SerialPortModel instance = null;
        private static readonly object instanceLock = new object();
        public static SerialPortModel Instance
        {
            get
            {
                lock(instanceLock)
                {
                    if(instance == null)
                    {
                        instance = new SerialPortModel();
                    }
                }
                return instance;
            }
        }
        #endregion

        
        public SerialPort activeSerialPort;

        private SerialPortModel()
        {
            activeSerialPort = new SerialPort();
            // Start port scanning in a background task
            Task.Run(() => ScanPortsAsync(_cancellationTokenSource.Token));
        }

        public void Open(string portName)
        {
            activeSerialPort = new SerialPort(portName, 115200);
            try
            {
                activeSerialPort.Open();
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Access to the port is denied: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error opening the port: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid port name or parameters: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            if (activeSerialPort.IsOpen)
            {
                Console.WriteLine("Successfully opened port");
            }
        }

        public void Close()
        {
            activeSerialPort.Close();
        }

        public void Write(string data)
        {
            activeSerialPort.Write(data);
        }

        public static string GetLSBString(int input, int bytes)
        {
            if (bytes != 1 && bytes != 2 && bytes != 4)
            {
                throw new ArgumentException("The number of bytes must be 1, 2, or 4.");
            }

            // Create a byte array with the specified number of bytes
            byte[] byteArray = BitConverter.GetBytes(input);

            // Extract only the number of bytes needed
            byte[] relevantBytes = new byte[bytes];
            Array.Copy(byteArray, 0, relevantBytes, 0, bytes);

            // Convert the byte array to a hexadecimal string
            return BitConverter.ToString(relevantBytes).Replace("-", "").ToLower(CultureInfo.InvariantCulture);
        }
        public void SetLEDColor(byte r, byte g, byte b)
        {
            //this assembly should go somewhere else
            string msg = "";
            msg += GetLSBString(0x3001, 2);
            msg += GetLSBString(3, 2);
            msg += r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            msg += GetLSBString(0, 2);
            activeSerialPort.Write(msg);
        }

        public void SetLEDState(int state)
        {
            string msg = "";
            msg += GetLSBString(0x3003, 2);
            msg += GetLSBString(1, 2);
            msg += GetLSBString(state, 1);
            msg += GetLSBString(0, 2);
            activeSerialPort.Write(msg);
        }

        #region PortSearching
        private readonly object _portListLock = new object();
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private const int ScanIntervalMs = 1000; // 10 seconds
        public ObservableCollection<string> PortList = new ObservableCollection<string>();
        public async void StartAutoSearch()
        {
            PortList.Clear();
            // Get all available serial ports
            string[] ports = SerialPort.GetPortNames();

            // Create a list to hold the tasks
            var tasks = ports.Select(portName => Task.Run(async () =>
            {
                try
                {
                    // Open the serial port
                    using (var serialPort = new SerialPort(portName, 115200))
                    {
                        serialPort.Open();

                        // Send the message and await the response
                        await SendMessageAndWaitForResponseAsync(serialPort, "020000000000", "02002000417869734472697665722056302E3000010000000000B443E9560000C85500000000");

                        // Add to PortList if the response matches
                        lock (_portListLock)
                        {
                            // Here you might want to check if PortList already contains the port
                            // if you only want to add it once
                            if (!PortList.Contains(portName))
                            {
                                PortList.Add(portName);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle exceptions related to opening or communicating with the serial port
                    Console.WriteLine($"Error with port {portName}: {ex.Message}");
                }
            })).ToList();

            // Wait for all tasks to complete
            await Task.WhenAll(tasks);      
            OnPropertyChanged(nameof(PortList));    
        }
        private async Task SendMessageAndWaitForResponseAsync(SerialPort serialPort, string message, string expectedResponse, int timeoutMs = 1000)
        {
            // Send the message
            serialPort.WriteLine(message);

            // Start a task to read the response
            string response = await Task.Run(() =>
            {
                DateTime startTime = DateTime.Now;
                var responseBuilder = new System.Text.StringBuilder();

                while ((DateTime.Now - startTime).TotalMilliseconds < timeoutMs)
                {
                    try
                    {
                        string line = serialPort.ReadLine().Trim();
                        responseBuilder.AppendLine(line);

                        if (line.Contains(expectedResponse))
                        {
                            return responseBuilder.ToString();
                        }
                    }
                    catch (TimeoutException)
                    {
                        // Ignore timeout exception and continue waiting
                    }
                }

                return responseBuilder.ToString();
            });

            // Check if the response matches the expected response
            if (response.Contains(expectedResponse))
            {
                // The response matches
            }
        }
        private async Task ScanPortsAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    string[] ports = SerialPort.GetPortNames();
                    Console.WriteLine("Scanning COM ports...");

                    // Use Dispatcher to ensure this is run on the UI thread
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        lock (_portListLock)
                        {
                            // Clear the existing PortList if you want to replace it
                            foreach (var port in ports)
                            {
                                Console.WriteLine($"Found port: {port}");

                                // Add to PortList
                                if (!PortList.Contains(port))
                                {
                                    PortList.Add(port);
                                }
                            }

                            foreach (var port in PortList)
                            {
                                if (!ports.Contains(port))
                                {
                                    PortList.Remove(port);
                                }
                            }

                            OnPropertyChanged(nameof(PortList));
                        }
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during port scanning: {ex.Message}");
                }

                // Wait for the next scan interval
                await Task.Delay(ScanIntervalMs, cancellationToken);
            }
        }
        public void StopScanning()
        {
            _cancellationTokenSource.Cancel();
        }
        #endregion

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Call this method to stop scanning when needed
        
    }
}
