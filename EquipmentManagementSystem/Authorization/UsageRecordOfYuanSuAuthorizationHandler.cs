using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Authorization
{
    public class UsageRecordOfYuanSuAuthorizationHandler :
        AuthorizationHandler<OperationAuthorizationRequirement, UsageRecordOfYuanSu>
    {
        // 元素项目使用记录权限控制，如果修改的话，记得同时更新液质使用记录的权限控制
        public UsageRecordOfYuanSuAuthorizationHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                        OperationAuthorizationRequirement requirement,
                                                        UsageRecordOfYuanSu resource)
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
                else if (context.User.IsInRole(Constants.PrincipalRole) && resource.GroupName == context.User.FindFirst("Group")?.Value)
                {
                    context.Succeed(requirement);
                }
                else if (context.User.IsInRole(Constants.TechnicianRole) && 
                    resource.GroupName == context.User.FindFirst("Group")?.Value
                    /*&&  resource.Operator == context.User.FindFirst(ClaimTypes.GivenName)?.Value*/)
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
                //else if (context.User.IsInRole(Constants.PrincipalRole) && resource.GroupName == context.User.FindFirst("Group")?.Value)
                //{
                //    context.Succeed(requirement);
                //}
                //else if (context.User.IsInRole(Constants.TechnicianRole) && resource.Operator == context.User.FindFirst(ClaimTypes.GivenName)?.Value)
                //{
                //    context.Succeed(requirement);
                //}
            }

            return Task.CompletedTask;
        }
    }
}
