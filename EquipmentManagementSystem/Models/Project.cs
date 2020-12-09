using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EquipmentManagementSystem.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name = "项目名称")]
        public string Name { get; set; }

        [Display(Name = "简称")]
        public string ShortName { get; set; }

        [Display(Name = "所属项目组")]
        public string GroupName { get; set; }

        [Display(Name = "单个样品检测时间")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0: HH:mm:ss}")]
        public DateTime? SimpleTestTime { get; set; }

        [Display(Name = "色谱柱类型")]
        public string ColumnType { get; set; }

        [Display(Name = "流动相/载气")]
        public string MobilePhase { get; set; }

        [Display(Name = "离子源类型")]
        public string IonSource { get; set; }

        [Display(Name = "检测器")]
        public string Detector { get; set; }

        public void SetColumnType(List<string> columntypes)
        {
            columntypes.RemoveAll(l => string.IsNullOrEmpty(l));
            ColumnType = string.Join("|", columntypes);
        }
        public List<string> GetColumnType()
        {
            return ColumnType?.Split("|").ToList() ?? new List<string>();
        }

        public void SetMobilePhase(List<string> mobilePhases)
        {
            mobilePhases.RemoveAll(l => string.IsNullOrEmpty(l));
            MobilePhase = string.Join("|", mobilePhases);
        }
        public List<string> GetMobilePhase()
        {
            return MobilePhase?.Split("|").ToList() ?? new List<string>();
        }

        public void SetIonSource(List<string> ionSources)
        {
            ionSources.RemoveAll(l => string.IsNullOrEmpty(l));
            IonSource = string.Join("|", ionSources);
        }
        public List<string> GetIonSource()
        {
            return IonSource?.Split("|").ToList() ?? new List<string>();
        }

        public void SetDetector(List<string> detectors)
        {
            detectors.RemoveAll(l => string.IsNullOrEmpty(l));
            Detector = string.Join("|", detectors);
        }
        public List<string> GetDetector()
        {
            return Detector?.Split("|").ToList() ?? new List<string>();
        }
    }
}
