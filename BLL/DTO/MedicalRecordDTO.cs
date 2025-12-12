using System;
using System.Collections.Generic;

namespace Catalog.BLL.DTOs
{
    public class MedicalRecordDTO
    {
        public int RecordId { get; set; }
        public int EmployeeId { get; set; }
        public string History { get; set; }
        public string Type { get; set; }
        public string DeseaseType { get; set; }
        public DateTime LastCheckup { get; set; }

        public IEnumerable<MedicalCheckupDTO> MedicalCheckups { get; set; }
    }
}