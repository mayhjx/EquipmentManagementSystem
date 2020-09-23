﻿using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EquipmentManagementSystem.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        private readonly UserManager<User> _userManager;
        public UserResolverService(IHttpContextAccessor context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public string GetUserId()
        {
            return _context.HttpContext?.User?.Identity?.Name;
        }

        public string GetUserName()
        {

            return _userManager.GetUserAsync(_context.HttpContext?.User).Result?.Name;
        }
    }
}
