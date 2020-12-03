using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EquipmentManagementSystem.Authorization
{
    public class MaintenanceRecordAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, MaintenanceRecord>
    {
        private readonly UserManager<User> _userManager;

        public MaintenanceRecordAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        MaintenanceRecord resource)
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
                else if (context.User.IsInRole(Constants.PrincipalRole))
                {
                    if (_userManager.GetUserAsync(context.User).Result.Group == resource.Project.Group.Name)
                    {
                        context.Succeed(requirement);
                    }
                }
                else if (context.User.IsInRole(Constants.TechnicianRole))
                {
                    if (resource.Operator == _userManager.GetUserAsync(context.User).Result.Name)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            if (requirement.Name == Constants.DeleteOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.PrincipalRole))
                {
                    if (_userManager.GetUserAsync(context.User).Result.Group == resource.Project.Group.Name)
                    {
                        context.Succeed(requirement);
                    }
                }
                else if (context.User.IsInRole(Constants.TechnicianRole))
                {
                    if (resource.Operator == _userManager.GetUserAsync(context.User).Result.Name)
                    {
                        context.Succeed(requirement);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
