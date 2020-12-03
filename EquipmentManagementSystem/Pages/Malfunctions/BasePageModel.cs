using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EquipmentManagementSystem.Pages.Malfunctions
{
    public class BasePageModel : PageModel
    {
        protected EquipmentContext _context { get; }
        protected IAuthorizationService _authorizationService { get; }
        protected UserManager<User> _userManager { get; }

        public BasePageModel(
            EquipmentContext context,
            IAuthorizationService authorizationService,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }
    }
}
