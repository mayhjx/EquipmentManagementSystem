using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization.Malfunction
{
    public class WorkOrderAuthorizationHandler
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


            if (requirement.Name == Constants.ReadOperationName)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Constants.CreateOperationName)
            {
                // 前端返回用户所属项目组所有检测项目相关的检测仪器，即只能新建自己组内项目需要用到的仪器
                context.Succeed(requirement);
            }

            if (requirement.Name == Constants.UpdateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) || context.User.IsInRole(Constants.ManagerRole) || context.User.IsInRole(Constants.SupervisorRole))
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.PrincipalRole) && resource.Instrument.Group == context.User.FindFirst("Group")?.Value)
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.TechnicianRole) &&
                    resource.Instrument.Group == context.User.FindFirst("Group")?.Value &&
                    resource.Creator == context.User.FindFirst(ClaimTypes.GivenName)?.Value)
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
