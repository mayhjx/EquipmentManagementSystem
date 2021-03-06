using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization
{
    public class InstrumentAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, Instrument>
    {
        public InstrumentAuthorizationHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        Instrument resource)
        {
            if (context.User == null || resource == null)
            {
                return Task.CompletedTask;
            }

            if (requirement.Name == Constants.ReadOperationName)
            {
                if(!context.User.IsInRole(Constants.TechnicianRole))
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.CreateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole))
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.Name == Constants.UpdateOperationName)
            {
                if (context.User.IsInRole(Constants.DirectorRole) ||
                    context.User.IsInRole(Constants.ManagerRole) ||
                    context.User.IsInRole(Constants.SupervisorRole))
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
