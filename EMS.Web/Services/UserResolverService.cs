using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EquipmentManagementSystem.Services
{
    /// <summary>
    /// 获取用户名和ID
    /// </summary>
    public class UserResolverService : IUserResolverService
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> _userManager;
        public UserResolverService(IHttpContextAccessor context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string GetUserGroup()
        {
            if (_context.HttpContext?.User == null)
            {
                return string.Empty;
            }
            return _userManager.GetUserAsync(_context.HttpContext?.User).Result?.Group;
        }

        public string GetUserId()
        {
            return _context.HttpContext?.User?.Identity?.Name;
        }

        public string GetUserName()
        {
            if (_context.HttpContext?.User == null)
            {
                return string.Empty;
            }
            return _userManager.GetUserAsync(_context.HttpContext?.User).Result?.Name;
        }
    }
}
