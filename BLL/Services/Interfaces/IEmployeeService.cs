using System.Collections.Generic;
using Catalog.BLL.DTOs;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {
        EmployeeDTO GetByUserId(int userId);
        IEnumerable<EmployeeDTO> GetAllEmployees();
        IEnumerable<MedicalRecordDTO> GetMedicalRecordsForEmployee(int employeeId);
    }
}