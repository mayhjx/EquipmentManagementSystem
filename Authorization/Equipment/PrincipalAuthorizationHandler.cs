using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization.Equipment
{
    public class PrincipalAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, Instrument>
    {
        UserManager<User> _userManager;
        public PrincipalAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        Instrument resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName)
            {
                return Task.CompletedTask;
            }

            var group = _userManager.GetUserAsync(context.User).Result.Group;

            if (resource.Group == group && context.User.IsInRole(Constants.PrincipalRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
