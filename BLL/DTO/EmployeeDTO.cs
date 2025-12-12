using System;
using System.Collections.Generic;

namespace Catalog.BLL.DTOs
{
    public class EmployeeDTO
    {
        public int UserId { get; set; }
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }

        public IEnumerable<MedicalRecordDTO> MedicalRecords { get; set; }
    }
}
