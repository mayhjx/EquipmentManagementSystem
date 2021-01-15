using EquipmentManagementSystem.Areas.Identity.Data;
using EquipmentManagementSystem.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace EquipmentManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            //UpdateProjectsOfInstrument(host);
            //CreateDbIfNotExists(host);
            //CreateAdminIfNotExists(host);
            //CreateUserIfNotExists(host);
            host.Run();
        }

        // 修改Instrument的projects字段中的括号及其内容
        //private static void UpdateProjectsOfInstrument(IHost host)
        //{
        //    using var scope = host.Services.CreateScope();
        //    var services = scope.ServiceProvider;

        //    try
        //    {
        //        var context = services.GetRequiredService<EquipmentContext>();
        //        var wwwrootPath = services.GetRequiredService<IWebHostEnvironment>().WebRootPath;

        //        foreach (var instr in context.Instruments)
        //        {
        //            if (string.IsNullOrEmpty(instr.Projects)) continue;
        //            var projectList = instr.Projects.Split(", ");
        //            var projectRemoveShortName = new List<string>();
        //            foreach (var project in projectList)
        //            {
        //                var leftIndex = project.IndexOf("(");
        //                var rightIndex = project.IndexOf(")");
        //                if (leftIndex > 0)
        //                {
        //                    var left = project.Substring(0, leftIndex);
        //                    string right = "";
        //                    if (rightIndex < project.Length - 1)
        //                    {
        //                        right = project.Substring(rightIndex + 1, project.Length - 1 - rightIndex);
        //                    }
        //                    projectRemoveShortName.Add($"{left}{right}");
        //                }
        //            }
        //            if (projectRemoveShortName.Count > 0)
        //            {
        //                instr.Projects = string.Join(", ", projectRemoveShortName);
        //            }
        //        }
        //        context.SaveChanges();
        //        //DbInitializer.Initialize(context, wwwrootPath);
        //    }
        //    catch (Exception ex)
        //    {
        //        var logger = services.GetRequiredService<ILogger<Program>>();
        //        logger.LogError(ex, "An error occurred creating the DB.");
        //        //throw ex;
        //    }
        //}

        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<EquipmentContext>();
                var wwwrootPath = services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
                // Create the DB if no DB exists 不用老是在命令行输入dotnet ef...
                // context.Database.EnsureCreated(); 
                DbInitializer.Initialize(context, wwwrootPath);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred creating the DB.");
                //throw ex;
            }
        }

        public static void CreateAdminIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    AdminInitialize.InitializeAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the Admin.");
                }
            }
        }

        public static void CreateUserIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    UserInitialize.InitializeAsync(services).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the Admin.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
