namespace EMS.Core.Entities.CalibrationAggregate
{
    public class CalibrationUploadFile : BaseEntity
    {
        public int CalibrationId { get; set; }
        public Calibration Calibration { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
