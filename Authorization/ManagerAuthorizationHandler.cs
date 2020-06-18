using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization
{
    public class ManagerAuthorizationHandler
                : AuthorizationHandler<OperationAuthorizationRequirement, MalfunctionWorkOrder>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        MalfunctionWorkOrder resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name != Constants.CreateOperationName &&
                requirement.Name != Constants.ReadOperationName &&
                requirement.Name != Constants.UpdateOperationName &&
                requirement.Name != Constants.DeleteOperationName &&
                requirement.Name != Constants.ComfirmOperationName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.ManagerRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
