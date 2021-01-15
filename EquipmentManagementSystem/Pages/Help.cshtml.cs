using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EquipmentManagementSystem.Pages
{
    [AllowAnonymous]
    public class HelpModel : PageModel
    {
        private readonly ILogger<HelpModel> _logger;

        public HelpModel(ILogger<HelpModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
