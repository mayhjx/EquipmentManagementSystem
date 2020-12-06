using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;

namespace EquipmentManagementSystem.Pages.Management.Projects
{
    public class BasePageModel: PageModel
    {
        protected readonly IProjectRepository _projectRepository;

        public BasePageModel(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
    }
}
