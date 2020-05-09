using Microsoft.Extensions.Hosting;
using SmartHouse.EntityCore.Context;
using SmartHouse.EntityCore.Helpers;
using SmartHouse.EntityCore.Model;
using SmartHouse.Helpers;
using SmartHouse.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHouse.BackgroundWorkers
{
    public class ArduinoDataGetter : BackgroundService
    {
        public static DataModel Model;

        private PortHelper portHelper;
        private DbHelper dbHelper;

        public ArduinoDataGetter(DbHelper context)
        {
            dbHelper = context;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                await Task.Delay(2000);

                if (!IsPortSelected)
                {
                    continue;
                }

                if (portHelper == null)
                {
                    portHelper = new PortHelper();
                }
          
                Model = InitDataModel();

                dbHelper.Add(Model);
            }
        }

        #region Implimentation 
        private bool IsPortSelected { get => !string.IsNullOrEmpty(CustomPageModel.selected_port); }
        private DataModel InitDataModel()
        {
            DataModel dataModel = new DataModel();
            try
            {
                portHelper.ComPort.Open();
                portHelper.ComPort.DiscardOutBuffer();
                portHelper.ComPort.Write("1");
                Thread.Sleep(1000); // need to arduino process request

                string data = portHelper.ComPort.ReadExisting();

                if (data.Length <= 0)
                {
                    return dataModel;
                }

                dataModel.Temperature = data.Substring(8, 5);
                dataModel.Wetness = data.Substring(0, 5);

                portHelper.ComPort.Close();
            }
            catch { }
            return dataModel;
        }
        #endregion
    }
}
