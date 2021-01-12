using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization.Malfunction
{
    public class MalfunctionInfoAuthorizationHandler
        : AuthorizationHandler<OperationAuthorizationRequirement, MalfunctionInfo>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        MalfunctionInfo resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name == Constants.UpdateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.PrincipalRole) && 
                    resource.MalfunctionWorkOrder.Instrument.Group == context.User.FindFirst("Group")?.Value)
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.TechnicianRole) &&
                    resource.MalfunctionWorkOrder.Instrument.Group == context.User.FindFirst("Group")?.Value &&
                    resource.MalfunctionWorkOrder.Creator == context.User.FindFirst(ClaimTypes.GivenName)?.Value)
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.ComfirmOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole))
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.PrincipalRole) && 
                    resource.MalfunctionWorkOrder.Instrument.Group == context.User.FindFirst("Group")?.Value)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
