using System.Threading.Tasks;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EquipmentManagementSystem.Authorization
{
    public class AcceptanceAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, InstrumentAcceptance>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        InstrumentAcceptance resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name == Constants.CreateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole))
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
                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.DeleteOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
