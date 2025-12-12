using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Moq;
using Xunit;
using Catalog.BLL.Mapping;
using Catalog.BLL.Services.Impl;
using Catalog.DAL.UnitOfWork;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.Entities;

namespace Catalog.BLL.Tests
{
    public class EmployeeServiceTests
    {
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            return config.CreateMapper();
        }

        [Fact]
        public void Ctor_InputNullIUnitOfWork_ThrowArgumentNullException()
        {
            IUnitOfWork nullUnitOfWork = null;
            var mapper = CreateMapper();

            Assert.Throws<ArgumentNullException>(() => new EmployeeService(nullUnitOfWork, mapper));
        }

        [Fact]
        public void GetByUserId_UserIsEmployee_ReturnsEmployeeDTO()
        {
            var mapper = CreateMapper();

            var medicalRecord = new MedicalRecord
            {
                recordId = 100,
                employeeId = 20,
                history = "history",
                type = "annual",
                deseaseType = "none",
                lastCheckup = DateTime.UtcNow
            };

            var employee = new Employee
            {
                userId = 2,
                employeeId = 20,
                name = "Emp Name",
                position = "Engineer",
                department = "Plant",
                MedicalRecords = new List<MedicalRecord> { medicalRecord }
            };

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.Get(It.IsAny<int>())).Returns(employee);
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User> { employee });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new EmployeeService(mockUow.Object, mapper);

            var dto = service.GetByUserId(2);

            Assert.NotNull(dto);
            Assert.Equal(employee.userId, dto.UserId);
            Assert.Equal(employee.employeeId, dto.EmployeeId);
            Assert.Equal(employee.name, dto.Name);
            Assert.Equal(employee.position, dto.Position);
            Assert.Equal(employee.department, dto.Department);
            Assert.NotNull(dto.MedicalRecords);
            Assert.Single(dto.MedicalRecords);
            var rec = dto.MedicalRecords.First();
            Assert.Equal(medicalRecord.recordId, rec.RecordId);
            Assert.Equal(medicalRecord.history, rec.History);
        }

        [Fact]
        public void GetAllEmployees_ReturnsOnlyEmployeesMapped()
        {
            var mapper = CreateMapper();

            var u1 = new User { userId = 1, name = "User1", email = "u1@x" };
            var e1 = new Employee { userId = 3, employeeId = 30, name = "Emp1", position = "P", department = "D" };

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User> { u1, e1 });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new EmployeeService(mockUow.Object, mapper);

            var dtos = service.GetAllEmployees().ToList();

            Assert.Single(dtos);
            Assert.Equal(e1.employeeId, dtos[0].EmployeeId);
            Assert.Equal(e1.name, dtos[0].Name);
        }

        [Fact]
        public void GetMedicalRecordsForEmployee_EmployeeNotFound_ReturnsEmpty()
        {
            var mapper = CreateMapper();

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User>());

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new EmployeeService(mockUow.Object, mapper);

            var records = service.GetMedicalRecordsForEmployee(999);

            Assert.NotNull(records);
            Assert.Empty(records);
        }
    }
}