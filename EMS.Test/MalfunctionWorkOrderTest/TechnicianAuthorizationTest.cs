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
    public class TechnicianAuthorizationTest
    {
        [Fact]
        public async Task Handler_MalfunctionWorkOrder_Read_ShouldSucceed()
        {
            var resource = new MalfunctionWorkOrder { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateMalfunctionWorkOrderWithOwned_ShouldSucceed()
        {
            var resource = new MalfunctionWorkOrder { Creator = "Test", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test"),
                new Claim("Group", "Test"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateMalfunctionWorkOrderWithIllegalUser_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { Creator = "Test 1" , Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateMalfunctionInfo_ShouldFail()
        {
            var resource = new MalfunctionInfo { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new MalfunctionInfoAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateInvestigation_ShouldFail()
        {
            var resource = new Investigation { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InvestigationAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateRepairRequest_ShouldFail()
        {
            var resource = new RepairRequest { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new RepairRequestAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateAccessoriesOrder_ShouldFail()
        {
            var resource = new AccessoriesOrder { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new AccessoriesOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateRepair_ShouldFail()
        {
            var resource = new Repair { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new RepairAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_UpdateValidation_ShouldFail()
        {
            var resource = new Validation { MalfunctionWorkOrder = new MalfunctionWorkOrder { Creator = "Test 1", Instrument = new Instrument { ID = "FXS-YZ01", Group = "Test" } } };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test 2"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new ValidationAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_DeleteMalfunctionWorkOrder_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { Creator = "Test" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
                new Claim(ClaimTypes.GivenName, "Test"),
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new WorkOrderAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        //[Fact]
        //public async Task Handler_DeleteMalfunctionWorkOrderWithIllegalUser_ShouldFail()
        //{
        //    var resource = new MalfunctionWorkOrder { Creator = "Test 1" };
        //    var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
        //        new Claim(ClaimTypes.Role, Constants.TechnicianRole),
        //        new Claim(ClaimTypes.GivenName, "Test 2"),
        //    }));
        //    var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

        //    var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
        //    var authzHandler = new WorkOrderAuthorizationHandler();
        //    await authzHandler.HandleAsync(authzContext);

        //    Assert.False(authzContext.HasSucceeded);
        //}


        // 故障信息相关

        [Fact]
        public async Task Handler_ComfirmMalfunctionInfomation_ShouldFail()
        {
            var resource = new MalfunctionWorkOrder { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.TechnicianRole),
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
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
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.TechnicianRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new ValidationAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }
    }
}
