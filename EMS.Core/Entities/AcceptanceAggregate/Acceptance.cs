using System;
using System.ComponentModel.DataAnnotations;

namespace EMS.Core.Entities.AcceptanceAggregate
{
    public class Acceptance : BaseEntity
    {
        [Required]
        [StringLength(20)]
        [Display(Name = "设备编号")]
        public string InstrumentNumber { get; set; }
        public int InstrumentId { get; set; }
        public Instrument Instrument { get; set; }

        [StringLength(10)]
        [Display(Name = "跟进人")]
        public string Operator { get; set; }

        [Display(Name = "设备安装说明")]
        public AcceptanceUploadFile InstallationNote { get; set; }

        [Display(Name = "预计到货日期")]
        [DataType(DataType.Date)]
        public DateTime? EstimatedArrivalDate { get; set; }

        [Display(Name = "实际到货日期")]
        [DataType(DataType.Date)]
        public DateTime? ArrivalDate { get; set; }

        [Display(Name = "是否齐全？")]
        public bool IsInventoryComplete { get; set; }

        [Display(Name = "设备清点证明")]
        public AcceptanceUploadFile InventoryCertificate { get; set; }

        [Display(Name = "备注")]
        [StringLength(1000)]
        public string InventoryRemark { get; set; }

        [Display(Name = "安装日期")]
        [DataType(DataType.Date)]
        public DateTime? InstallationDate { get; set; }

        [Display(Name = "备注")]
        [StringLength(1000)]
        public string InstallationRemark { get; set; }

        [Display(Name = "厂家验收合格")]
        public bool IsFactoryAcceptance { get; set; }

        [Display(Name = "厂家验收日期")]
        [DataType(DataType.Date)]
        public DateTime? FactoryAcceptanceDate { get; set; }

        [Display(Name = "设备调试验收报告")]
        public AcceptanceUploadFile FactoryAcceptanceReport { get; set; }

        [Display(Name = "设备安装服务报告")]
        public AcceptanceUploadFile ServiceReport { get; set; }

        [Display(Name = "备注")]
        [StringLength(1000)]
        public string FactoryAcceptanceRemark { get; set; }

        [Display(Name = "使用及维护培训")]
        public bool IsTrainingUseAndMaintenance { get; set; }

        [Display(Name = "培训签到表")]
        public AcceptanceUploadFile TrainingSignInForm { get; set; }

        [Display(Name = "自行搭建")]
        public bool IsSelfBuilt { get; set; }

        [Display(Name = "应用协助")]
        public bool IsEngineerAssistance { get; set; }

        [Display(Name = "备注")]
        [StringLength(1000)]
        public string MethodConstructionRemark { get; set; }

        [Display(Name = "项目验收合格")]
        public bool IsAcceptance { get; set; }

        [Display(Name = "项目验收日期")]
        [DataType(DataType.Date)]
        public DateTime? ItemAcceptanceDate { get; set; }

        [Display(Name = "评估报告")]
        public AcceptanceUploadFile EvaluationReport { get; set; }

        [Display(Name = "仪器设备履历表")]
        public AcceptanceUploadFile EquipmentResume { get; set; }

        [Display(Name = "仪器设备档案目录表")]
        public AcceptanceUploadFile EquipmentFilesList { get; set; }

        [Display(Name = "产品合格证")]
        public AcceptanceUploadFile EquipmentCertificate { get; set; }

        [Display(Name = "厂家生产许可证")]
        public AcceptanceUploadFile FactoryProductionLicense { get; set; }

        [Display(Name = "营业执照")]
        public AcceptanceUploadFile BusinessLicense { get; set; }

        [Display(Name = "医疗器械注册证")]
        public AcceptanceUploadFile MedicalDeviceRegistrationCertificate { get; set; }

        [Display(Name = "校准/检定证书")]
        public AcceptanceUploadFile EquipmentCalibrationReport { get; set; }

        [Display(Name = "设备验收报告")]
        public AcceptanceUploadFile EquipmentAcceptanceReport { get; set; }

        [Display(Name = "设备验收日期")]
        [DataType(DataType.Date)]
        public DateTime? Date { get; set; }
    }
}
