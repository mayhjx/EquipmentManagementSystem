using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization
{
    public class DirectorAuthorizationHandler
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
                requirement.Name != Constants.ApproveOperationName)
            {
                return Task.CompletedTask;
            }

            if (context.User.IsInRole(Constants.DirectorRole))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
