using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentManagementSystem.Pages.Instruments
{
    public class BasePageModel : PageModel
    {
        protected EquipmentContext _context { get; }
        protected UserManager<User> _userManager { get; }
        protected IAuthorizationService _authorizationService { get; }

        public BasePageModel(
            EquipmentContext context,
            UserManager<User> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
    }
}
