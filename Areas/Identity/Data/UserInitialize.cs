using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Areas.Identity.Data
{
    public class UserInitialize
    {
        private static string path;
        public static async Task InitializeAsync(IServiceProvider services)
        {
            var wwwrootPath = services.GetRequiredService<IWebHostEnvironment>().WebRootPath;
            var userManager = services.GetRequiredService<UserManager<User>>();

            path = Path.Combine(wwwrootPath, "Users.csv");

            string[] Datas = Reader(path);

            foreach (var line in Datas.Skip(1))
            {
                if (line.Trim() == "") continue;

                var data = line.Split(",");
                var user = await userManager.FindByNameAsync(data[1]);
                if (user != null) continue;

                user = new User
                {
                    UserName = data[0],
                    Name = data[1],
                    Group = data[2],
                    Number = data[0]
                };

                await userManager.CreateAsync(user, data[4]);
                await userManager.AddToRoleAsync(user, data[3]);
            }
        }

        private static string[] Reader(string filepath)
        {
            string[] text = { };
            if (string.IsNullOrEmpty(filepath) || !filepath.EndsWith(".csv"))
            {
                return text;
            }
            using (var sr = new StreamReader(filepath, Encoding.Default))
            {
                text = sr.ReadToEnd().Split("\r\n");
            }
            return text;
        }
    }
}
