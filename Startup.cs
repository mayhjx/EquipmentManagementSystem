using EquipmentManagementSystem.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace EquipmentManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            var storedFilesPath = Configuration.GetValue<string>("StoredFilesPath");
            if (!Directory.Exists(storedFilesPath))
            {
                Directory.CreateDirectory(storedFilesPath);
            }

            var physicalProvider = new PhysicalFileProvider(storedFilesPath);
            services.AddSingleton<IFileProvider>(physicalProvider);

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<EquipmentContext>(options =>
                    options.UseSqlite(Configuration.GetConnectionString("EquipmentContext")));

                services.AddDbContext<MalfunctionContext>(options =>
                        options.UseSqlite(Configuration.GetConnectionString("EquipmentContext")));
            }
            else
            {
                services.AddDbContext<EquipmentContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("EquipmentContext")));

                services.AddDbContext<MalfunctionContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("EquipmentContext")));
            }

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
