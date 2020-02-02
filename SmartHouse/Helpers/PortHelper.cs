using SmartHouse.Models;
using System.IO.Ports;

namespace SmartHouse.Helpers
{
    public class PortHelper
    {
        public SerialPort ComPort;
        public PortHelper()
        {
            ComPort = new SerialPort
            {
                PortName = CustomPageModel.selected_port,
                BaudRate = 9600,
                Parity = Parity.None,
                StopBits = StopBits.One,
                DataBits = 8,
                Handshake = Handshake.None,
                ReadTimeout = 500
            };
        }
    }
}
