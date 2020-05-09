using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using SmartHouse.Models;
using SmartHouse.Services;

namespace SmartHouse
{
    //[Authorize]
    public class AdminModel : CustomPageModel
    {
        public List<string> Ports { get; set; }

        #region Services Initializer
        private ComPortService comPortService; 
        public AdminModel()
        {
            comPortService = new ComPortService();
            Ports = comPortService.GetComPorts();
        }
        #endregion
    }
}