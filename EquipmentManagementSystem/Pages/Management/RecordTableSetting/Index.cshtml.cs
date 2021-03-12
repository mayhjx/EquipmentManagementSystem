using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EquipmentManagementSystem.Models;
using EquipmentManagementSystem.Data;
using EquipmentManagementSystem.Interfaces;

namespace EquipmentManagementSystem.Pages.Management.RecordTableSetting
{
    public class IndexModel : PageModel
    {
        private readonly IRecordTableSettingRepository _recordTableSettingRepository;
        private readonly IInstrumentRepository _instrumentRepository;

        public IndexModel(IInstrumentRepository instrumentRepository,
            IRecordTableSettingRepository recordTableSettingRepository)
        {
            _recordTableSettingRepository = recordTableSettingRepository;
            _instrumentRepository = instrumentRepository;
        }

        [BindProperty]
        public Models.RecordTableSetting RecordTableSetting { get; set; }

        [BindProperty]
        public Models.RecordTableSetting RecordTableSettingToUpdate { get; set; }

        public IList<Models.RecordTableSetting> TableSettings { get; private set; }

        public List<string> InstrumentIds { get; private set; }

        public async Task OnGet()
        {
            TableSettings = await _recordTableSettingRepository.GetAll();
            InstrumentIds = _instrumentRepository.GetAllInstrumentId();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if(id == null)
            {
                // create
                await _recordTableSettingRepository.Create(RecordTableSetting);
            }
            else
            {
                // edit
                await _recordTableSettingRepository.Update(RecordTableSettingToUpdate);
            }

            return RedirectToAction("./Index");
        }

        public async Task<JsonResult> OnPostDeleteAsync(int id)
        {
            // handler delete
            var recordToDelete = await _recordTableSettingRepository.GetById(id);

            if (recordToDelete == null)
            {
                return new JsonResult("not found") { StatusCode = 404 };
            }

            await _recordTableSettingRepository.Delete(recordToDelete);

            return new JsonResult("success") { StatusCode = 200 };
        }
    }
}
