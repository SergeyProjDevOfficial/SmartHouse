using System.Collections.Generic;
using System.IO.Ports;

namespace SmartHouse.Services
{
    public class ComPortService
    {
        public List<string> GetComPorts()
        {
            List<string> ports = new List<string>();

            var ComPortsNames = SerialPort.GetPortNames();

            foreach (var ComPortName in ComPortsNames)
            {
                ports.Add(ComPortName);
            }

            return ports;
        }
    }
}
