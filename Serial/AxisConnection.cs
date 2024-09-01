using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.Security.Policy;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace AxisConfigurator
{
    public sealed class AxisConnection
    {
        private static AxisConnection instance = null;
        private static readonly object padlock = new object();

        private static AxisConnection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new AxisConnection();
                    }
                    return instance;
                }
            }
        }

        private SerialPort serialPort;
        public Action<bool> PortConnectedAction;
        string portName = "COM5";
        int baudRate = 115200;

        public bool isConnected { get { return serialPort.IsOpen; } }
        private AxisConnection()
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

                //serialPort.Write("4103001166000000");
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
            //PortConnectedAction(serialPort.IsOpen);
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
            Connect();
            //this assembly should go somewhere else
            string msg = "";
            msg += GetLSBString(0x3001, 2);
            msg += GetLSBString(3, 2);
            msg += r.ToString("X2") + g.ToString("X2") + b.ToString("X2");
            msg += GetLSBString(0, 2);
            serialPort.Write(msg);
        }
    }
}
