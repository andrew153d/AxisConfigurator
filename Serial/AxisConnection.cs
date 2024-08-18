using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Security.Policy;

namespace AxisConfigurator
{
    public class AxisConnection
    {
        private SerialPort serialPort;
        public Action<bool> PortConnectedAction;
        string portName = "COM15";
        int baudRate = 115200;

        public bool isConnected { get { return serialPort.IsOpen; } }
        public AxisConnection()
        {
            serialPort = new SerialPort(portName, baudRate);
        }

        public void Disconnect()
        {
            if(!serialPort.IsOpen)
            {
                return;
            }

            serialPort.Close();

            PortConnectedAction(serialPort.IsOpen);
        }
        
        public void Connect()
        {
            // Define the COM port and baud rate
            try
            {
                // Initialize the SerialPort object

                if(serialPort.IsOpen)
                {
                    return;
                }

                // Configure additional settings if necessary (e.g., parity, data bits, stop bits)
                serialPort.Parity = Parity.None;
                serialPort.DataBits = 8;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;

                // Open the serial port
                serialPort.Open();


                // Optionally, you can output a message indicating success
                Console.WriteLine("Connected to " + portName + " at " + baudRate + " baud.");

                serialPort.Write("4103001166000000");
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
            PortConnectedAction(serialPort.IsOpen);
        }
    }
}
