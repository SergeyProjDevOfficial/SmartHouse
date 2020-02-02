using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartHouse.Models
{
    public class CustomPageModel : PageModel
    {
        public static string selected_port = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string SelectedPort { 
            get => selected_port;
            set => selected_port = value;
        }

    }
}
