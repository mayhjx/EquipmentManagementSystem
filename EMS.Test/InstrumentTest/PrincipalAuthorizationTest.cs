using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace EMS.Test.InstrumentTest
{
    public class PrincipalAuthorizationTest
    {
        [Fact]
        public async Task Handler_Instrument_Read_ShouldSucceed()
        {
            var resource = new Instrument { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InstrumentAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_Instrument_Create_ShouldFail()
        {
            var resource = new Instrument { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InstrumentAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_Instrument_Update_SameGroup_ShouldSucceed()
        {
            var resource = new Instrument { Group = "VD" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VD")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InstrumentAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.True(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_Instrument_Update_DifferentGroup_ShouldSucceed()
        {
            var resource = new Instrument { Group = "VD" };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim(ClaimTypes.Role, Constants.PrincipalRole),
                new Claim("Group", "VAE")
            }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InstrumentAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }

        [Fact]
        public async Task Handler_Instrument_Delete_ShouldFail()
        {
            var resource = new Instrument { };
            var user = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> { new Claim(ClaimTypes.Role, Constants.PrincipalRole) }));
            var requirement = new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };

            var authzContext = new AuthorizationHandlerContext(new List<IAuthorizationRequirement> { requirement }, user, resource);
            var authzHandler = new InstrumentAuthorizationHandler();
            await authzHandler.HandleAsync(authzContext);

            Assert.False(authzContext.HasSucceeded);
        }
    }
}
