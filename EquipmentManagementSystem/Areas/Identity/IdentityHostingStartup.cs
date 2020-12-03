using System;
using EquipmentManagementSystem.Authorization;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EquipmentManagementSystem.Areas.Identity.IdentityHostingStartup))]
namespace EquipmentManagementSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<User>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<IdentityContext>();

                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings.
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    // Lockout settings.
                    //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    //options.Lockout.MaxFailedAccessAttempts = 5;
                    //options.Lockout.AllowedForNewUsers = true;

                    // User settings.
                    options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = false;
                });

                services.ConfigureApplicationCookie(options =>
                {
                    // Cookie settings
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

                    options.LoginPath = "/Identity/Account/Login";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                    options.SlidingExpiration = true;
                });

                // Set the default authentication policy to require users to be authenticated
                services.AddControllers(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                });

                services.AddScoped<IAuthorizationHandler, TechnicianAuthorizationHandler>();
                services.AddScoped<IAuthorizationHandler, PrincipalAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, ManagerAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, DirectorAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, AdministratorAuthorizationHandler>();
                services.AddScoped<IAuthorizationHandler, InstrumentAuthorizationHandler>();
                services.AddScoped<IAuthorizationHandler, CalibrationAuthorizationHandler>();
                services.AddScoped<IAuthorizationHandler, UsageRecordAuthorizationHandler>();
                services.AddScoped<IAuthorizationHandler, MaintenanceRecordAuthorizationHandler>();
                services.AddSingleton<IAuthorizationHandler, AcceptanceAuthorizationHandler>();
            });
        }
    }
}
