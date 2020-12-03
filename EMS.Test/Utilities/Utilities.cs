using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMS.Test
{
    public static class Utilities
    {
        public static EquipmentContext CreateContext(DbContextOptions<EquipmentContext> options)
        {
            return new EquipmentContext(options, MockIUserResolverService());
        }

        #region snippet1
        public static DbContextOptions<EquipmentContext> TestDbContextOptions()
        {
            //Create a new service provider to create a new in-memory database.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance using an in-memory database and 
            // IServiceProvider that the context should resolve all of its 
            // services from.
            var builder = new DbContextOptionsBuilder<EquipmentContext>()
                .UseInMemoryDatabase("InMemoryDb")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        #endregion

        public static IUserResolverService MockIUserResolverService()
        {
            var userResolverService = new Mock<IUserResolverService>();
            userResolverService.Setup(i => i.GetUserId()).Returns("1");
            userResolverService.Setup(i => i.GetUserName()).Returns("Test");
            userResolverService.Setup(i => i.GetUserGroup()).Returns("VD");
            return userResolverService.Object;
        }

        public static UserManager<TUser> MockUserManager<TUser>(IUserStore<TUser> store = null)
    where TUser : class
        {
            // https://github.com/aspnet/Identity/blob/master/test/Shared/MockHelpers.cs
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(
                store: store,
                optionsAccessor: options.Object,
                passwordHasher: new PasswordHasher<TUser>(),
                userValidators: userValidators,
                passwordValidators: pwdValidators,
                keyNormalizer: new UpperInvariantLookupNormalizer(),
                errors: new IdentityErrorDescriber(),
                services: null,
                logger: new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }
    }
}
