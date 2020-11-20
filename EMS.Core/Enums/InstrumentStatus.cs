using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.Enum
{
    public enum InstrumentStatus
    {
        [Display(Name = "正常")]
        Normal,
        [Display(Name = "故障")]
        Malfunction,
        [Display(Name = "停用")]
        StopUsing,
        [Display(Name = "报废")]
        Scrapped
    }
}
