using Microsoft.Extensions.Hosting;
using SmartHouse.EntityCore.Context;
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
        //private SensorDataContext context;

        public ArduinoDataGetter()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                if (IsPostSelected) 
                {
                    if (portHelper == null)
                    {
                        portHelper = new PortHelper();
                        Model = new DataModel();
                    }
          
                    InitDataModel();
                }
                await Task.Delay(2000);
            }
        }

        #region Implimentation 
        private bool IsPostSelected { get => !string.IsNullOrEmpty(CustomPageModel.selected_port); }

        private delegate void TempAndWet();
        private void InitDataModel()
        {
            try
            {
                portHelper.ComPort.Open();
            
                #region Implimentation 
                TempAndWet InitTempAndWet = delegate () {
                    portHelper.ComPort.DiscardOutBuffer();
                    portHelper.ComPort.Write("1");
                    Thread.Sleep(1000); // need to arduino process request

                    string data = portHelper.ComPort.ReadExisting();

                    if (data.Length <= 0)
                    {
                        return;
                    }

                    Model.Temperature = data.Substring(8, 5);
                    Model.Wetness = data.Substring(0, 5);
                };
                #endregion

                InitTempAndWet();

                portHelper.ComPort.Close();
            }
            catch { }
        }
        #endregion
    }
}
