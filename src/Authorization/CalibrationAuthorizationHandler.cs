using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace EquipmentManagementSystem.Authorization
{
    public class CalibrationAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Calibration>
    {
        private readonly UserManager<User> _userManager;

        public CalibrationAuthorizationHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        Calibration resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }


            if (requirement.Name == Constants.CreateOperationName)
            {
                var currentUserGroup = _userManager.GetUserAsync(context.User).Result.Group ?? null;

                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole) ||
                    (context.User.IsInRole(Constants.PrincipalRole) && currentUserGroup == resource.Instrument.Group))
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.ReadOperationName)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Constants.UpdateOperationName)
            {
                //var currentUserGroup = _userManager.GetUserAsync(context.User).Result.Group ?? null;

                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.DeleteOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
