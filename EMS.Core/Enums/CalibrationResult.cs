using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.Enum
{
    public enum CalibrationResult
    {
        [Display(Name = "合格")]
        Passed,
        [Display(Name = "不合格")]
        Failed
    }
}
