using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Authorization.Malfunction;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.MalfunctionWorkOrderTest
{
    public class PrincipalAuthorizationTest
    {
        [Fact]
        public async Task Handler_MalfunctionWorkOrder_Read_ShouldSucceed()
        {
            var resource = new MalfunctionWorkOrder { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole)}));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_MalfunctionWorkOrder_Create_ShouldSucceed()
        {
            var resource = new MalfunctionWorkOrder { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateMalfunctionWorkOrderWithOwnedGroup_ShouldSucceed()
        {
            var resource = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VD")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateMalfunctionWorkOrderWithDifferentGroup_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VAE")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_DeleteMalfunctionWorkOrder_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VD")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        //[Fact]
        //public async Task Handler_DeleteMalfunctionWorkOrderWithDifferentGroup_ShouldFail()
        //{
        //    var resource = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } };
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
        //        new Claim(ClaimTypes.Role, Constants.PrincipalRole),
        //        new Claim("Group", "VAE")
        //    }));
        //    var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        //    var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
        //    var authzHandler = new WorkOrderAuthorizationHandler();
        //    await authzHandler.HandleAsync(authzContext);

        //    Assert.False(authzContext.HasSucceeded);
        //}

        [Fact]
        public async Task Handler_ComfirmMalfunctionInfomationWithOwnedGroup_ShouldSucceed()
        {
            var resource =new MalfunctionInfo { MalfunctionWorkOrder = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VD")
                }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ComfirmOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new MalfunctionInfoAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_ComfirmMalfunctionInfomationWithDifferentGroup_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { Instrument = new Instrument { Group = "VD" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole) ,
                new Claim("Group", "VAE")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ComfirmOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_ApproveRepairRequest_ShouldFail()
        {
            var resource = new RepairRequest { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new RepairRequestAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_ApproveValidationReport_ShouldFail()
        {
            var resource = new Validation { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new ValidationAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }
    }
}
