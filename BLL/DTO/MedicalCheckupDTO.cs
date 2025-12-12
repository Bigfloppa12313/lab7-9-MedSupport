using System;

namespace Catalog.BLL.DTOs
{
    public class MedicalCheckupDTO
    {
        public int CheckupId { get; set; }
        public DateTime Date { get; set; }
        public string Doctor { get; set; }
        public string Conclusion { get; set; }
    }
}