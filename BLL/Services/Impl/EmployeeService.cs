using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Catalog.BLL.DTOs;
using Catalog.BLL.Services.Interfaces;
using Catalog.DAL.UnitOfWork;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Services.Impl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public EmployeeDTO GetByUserId(int userId)
        {
            var user = _uow.Users.Get(userId);
            if (user == null) return null;
            if (user is Employee emp)
            {
                return _mapper.Map<Employee, EmployeeDTO>(emp);
            }
            return null;
        }

        public IEnumerable<EmployeeDTO> GetAllEmployees()
        {
            var users = _uow.Users.GetAll();
            var employees = users.OfType<Employee>().ToList();
            return _mapper.Map<IEnumerable<Employee>, List<EmployeeDTO>>(employees);
        }

        public IEnumerable<MedicalRecordDTO> GetMedicalRecordsForEmployee(int employeeId)
        {
            var userCandidates = _uow.Users.GetAll();
            var emp = userCandidates.OfType<Employee>().FirstOrDefault(e => e.employeeId == employeeId);
            if (emp == null) return Enumerable.Empty<MedicalRecordDTO>();

            var records = emp.MedicalRecords ?? Enumerable.Empty<MedicalRecord>();
            return _mapper.Map<IEnumerable<MedicalRecord>, List<MedicalRecordDTO>>(records);
        }
    }
}