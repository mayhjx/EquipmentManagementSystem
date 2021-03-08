using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.UsageRecordOfYuanSuTest
{
    public class TechnicianAuthorizationTest
    {
        [Fact]
        public async Task Handler_UsageRecordOfYuanSu_Read_ShouldSucceed()
        {
            var resource = new UsageRecordOfYuanSu { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UsageRecordOfYuanSu_Create_ShouldSucceed()
        {
            var resource = new UsageRecordOfYuanSu { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateUsageRecordWithOwningGroup_ShouldSucceed()
        {
            // 同一项目组内，技术员可以修改他人新建的记录
            var resource = new UsageRecordOfYuanSu { Operator = "User 2", GroupName ="Group"};
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "User 1"),
                new Claim("Group", "Group"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateUsageRecordWithIllegalGroup_ShouldFail()
        {
            // 技术员不可以修改其他项目组人员新建的记录
            var resource = new UsageRecordOfYuanSu { Operator = "Test 1", GroupName = "Group 2" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
                new Claim("Group", "Group 1"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_DeleteUsageRecordWithOwner_ShouldFail()
        {
            var resource = new UsageRecordOfYuanSu { Operator = "Test" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_DeleteUsageRecordWithIllegalUser_ShouldFail()
        {
            var resource = new UsageRecordOfYuanSu { Operator = "Test 1" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new UsageRecordOfYuanSuAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }
    }
}
