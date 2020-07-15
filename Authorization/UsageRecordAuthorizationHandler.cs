﻿using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EquipmentManagementSystem.Authorization
{
    public class UsageRecordAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, UsageRecord>
    {
        private readonly UserManager<User> _userManager;

        public UsageRecordAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        UsageRecord resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }


            if (requirement.Name == Constants.CreateOperationName)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Constants.ReadOperationName)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Constants.UpdateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
                else if (_userManager.GetUserAsync(context.User).Result.Group == resource.Project.Group.Name)
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.DeleteOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
                else if (_userManager.GetUserAsync(context.User).Result.Group == resource.Project.Group.Name)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
