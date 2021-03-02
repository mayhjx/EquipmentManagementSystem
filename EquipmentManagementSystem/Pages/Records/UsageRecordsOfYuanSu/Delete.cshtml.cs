using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentManagementSystem.Interfaces;
using EquipmentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using EquipmentManagementSystem.Authorization;
using Microsoft.EntityFrameworkCore;

namespace EquipmentManagementSystem.Pages.Records.UsageRecordsOfYuanSu
{
    public class DeleteModel : PageModel
    {
        private readonly IUsageRecordOfYuanSuRepository _usageRecordOfYuanSuRepository;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(IUsageRecordOfYuanSuRepository usageRecordOfYuanSuRepository,
            IAuthorizationService authorizationService)
        {
            _usageRecordOfYuanSuRepository = usageRecordOfYuanSuRepository;
            _authorizationService = authorizationService;
        }

        public void OnGet()
        {
        }

        [BindProperty]
        public UsageRecordOfYuanSu UsageRecordOfYuanSu { get; set; }

        public async Task<IActionResult> OnPost(int id)
        {
            UsageRecordOfYuanSu = await _usageRecordOfYuanSuRepository.GetById(id);

            if (UsageRecordOfYuanSu == null)
            {
                return new JsonResult("��¼δ�ҵ�����ˢ��ȷ�ϣ�");
            }

            //var isAuthorized = await _authorizationService.AuthorizeAsync(User, usageRecord, Operations.Delete);

            //if (!isAuthorized.Succeeded)
            //{
            //    return new JsonResult("��Ȩ�ޣ�");
            //}

            try
            {
                await _usageRecordOfYuanSuRepository.Delete(UsageRecordOfYuanSu);
                return new JsonResult("ɾ���ɹ���");
            }
            catch (DbUpdateException)
            {
                return new JsonResult("ɾ��ʧ�ܣ���ˢ�º����ԣ�");
            }
        }
    }
}
