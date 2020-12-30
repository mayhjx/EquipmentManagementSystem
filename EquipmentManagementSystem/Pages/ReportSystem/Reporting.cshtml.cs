using EquipmentManagementSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Pages.ReportSystem
{
    public class ReportingModel : PageModel
    {
        private readonly IReportService _reportService;
        private readonly IInstrumentRepository _instrumentRepository;
        public ReportingModel(IReportService reportService, IInstrumentRepository instrumentRepository)
        {
            _reportService = reportService;
            _instrumentRepository = instrumentRepository;
        }

        public void OnGet()
        {
            Search = new SearchForm();
            Search.InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();
        }

        [BindProperty]
        public SearchForm Search { get; set; }

        public List<double> MonthlyUsageHour { get; set; }
        public List<double> MonthlyMalfunctionHour { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MonthlyUsageHour = _reportService.GetMonthlyUsageHoursOfInstrument(Search.InstrumentId, Search.Year);
            MonthlyMalfunctionHour = _reportService.GetMonthlyMalfunctionHoursOfInstrument(Search.InstrumentId, Search.Year);

            Search.InstrumentSelectList = _instrumentRepository.GetAllInstrumentId();

            return Page();
        }

        public class SearchForm
        {
            [Required(ErrorMessage = "请输入年份")]
            [Display(Name = "年份")]
            public int Year { get; set; } = DateTime.Now.Year;

            [Required(ErrorMessage = "请选择一个设备编号")]
            [Display(Name = "设备编号")]
            public string InstrumentId { get; set; }

            public List<string> InstrumentSelectList { get; set; }
        }
    }
}
