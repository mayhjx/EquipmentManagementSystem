using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EquipmentManagementSystem.Pages
{
    [AllowAnonymous]
    public class ManualModel : PageModel
    {
        private readonly ILogger<ManualModel> _logger;

        public ManualModel(ILogger<ManualModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
