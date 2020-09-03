using System;
using System.ComponentModel.DataAnnotations;

namespace EquipmentManagementSystem.Models
{
    /// <summary>
    /// 设备验收信息
    /// </summary>
    public class InstrumentAcceptance
    {
        public int Id { get; set; }

        [Display(Name = "建单人")]
        public string Creator { get; set; }

        [Display(Name = "建单时间")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
        public DateTime? CreatedTime { get; set; }

        [Display(Name = "可行性报告")]
        public string FeasibilityReportFileName { get; set; }
        public string FeasibilityReportFilePath { get; set; }

        [Display(Name = "设备配置清单")]
        public string ConfigurationListFileName { get; set; }
        public string ConfigurationListFilePath { get; set; }

        [Display(Name = "设备采购申请单")]
        public string PuchaseRequisitionFileName { get; set; }
        public string PuchaseRequisitionFilePath { get; set; }

        [Display(Name = "Demo设备")]
        public bool IsDemo { get; set; }

        [Display(Name = "设备编号")]
        public string InstrumentID { get; set; }
        // Instrument详情界面关联InstrumentAcceptance

        [Display(Name = "设备安装说明")]
        public string InstallationNoteFileName { get; set; }
        public string InstallationNoteFilePath { get; set; }

        [Display(Name = "预计到货日期")]
        [DataType(DataType.Date)]
        public DateTime? EstimatedArrivalDate { get; set; }

        [Display(Name = "到货日期")]
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "是否齐全？")]
        public bool IsInventoryComplete { get; set; }

        [Display(Name = "清点证明")]
        public string InventoryCertificateFileName { get; set; }
        public string InventoryCertificateFilePath { get; set; }

        [Display(Name = "备注")]
        public string InventoryRemark { get; set; }

        [Display(Name = "安装日期")]
        [DataType(DataType.Date)]
        public DateTime? InstallationDate { get; set; }

        [Display(Name = "备注")]
        public string InstallationRemark { get; set; }

        [Display(Name = "厂家是否验收合格？")]
        public bool IsFactoryAcceptance { get; set; }

        [Display(Name = "厂家验收日期")]
        [DataType(DataType.Date)]
        public DateTime? FactoryAcceptanceDate { get; set; }

        [Display(Name = "设备调试验收证明")]
        public string FactoryAcceptanceCertificateFileName { get; set; }
        public string FactoryAcceptanceCertificateFilePath { get; set; }

        [Display(Name = "服务报告")]
        public string ServiceReportFileName { get; set; }
        public string ServiceReportFilePath { get; set; }

        [Display(Name = "使用及维护培训？")]
        public bool IsTrainingUseAndMaintenance { get; set; }

        [Display(Name = "培训签到表")]
        public string TrainingSignInFormFileName { get; set; }
        public string TrainingSignInFormFilePath { get; set; }

        [Display(Name = "自行搭建")]
        public bool IsSelfBuilt { get; set; }

        [Display(Name = "应用协助")]
        public bool IsEngineerAssistance { get; set; }

        [Display(Name = "备注")]
        public string MethodConstructionRemark { get; set; }

        [Display(Name = "项目是否验收合格？")]
        public bool IsAcceptance { get; set; }

        [Display(Name = "验收日期")]
        [DataType(DataType.Date)]
        public DateTime? AcceptanceDate { get; set; }

        [Display(Name = "评估报告")]
        public string EvaluationReportFileName { get; set; }
        public string EvaluationReportFilePath { get; set; }

        [Display(Name = "仪器设备履历表")]
        public string EquipmentResumeFileName { get; set; }
        public string EquipmentResumeFilePath { get; set; }

        [Display(Name = "仪器设备档案目录表")]
        public string EquipmentFilesListFileName { get; set; }
        public string EquipmentFilesListFilePath { get; set; }

        [Display(Name = "设备合格证")]
        public string EquipmentCertificateFileName { get; set; }
        public string EquipmentCertificateFilePath { get; set; }

        [Display(Name = "厂家生产许可证")]
        public string FactoryProductionLicenseFileName { get; set; }
        public string FactoryProductionLicenseFilePath { get; set; }

        [Display(Name = "营业执照")]
        public string BusinessLicenseFileName { get; set; }
        public string BusinessLicenseFilePath { get; set; }

        [Display(Name = "医疗器械注册证")]
        public string MedicalDeviceRegistrationCertificateFileName { get; set; }
        public string MedicalDeviceRegistrationCertificateFilePath { get; set; }

        [Display(Name = "设备校准报告")] // 能不能在设备校准模块新建？
        public string EquipmentCalibrationReportFileName { get; set; }
        public string EquipmentCalibrationReportFilePath { get; set; }

        [Display(Name = "设备验收报告")]
        public string EquipmentAcceptanceReportFileName { get; set; }
        public string EquipmentAcceptanceReportFilePath { get; set; }
    }
}
