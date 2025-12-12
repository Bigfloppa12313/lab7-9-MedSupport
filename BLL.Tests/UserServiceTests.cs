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
    public class UserServiceTests
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

            Assert.Throws<ArgumentNullException>(() => new UserService(nullUnitOfWork, mapper));
        }

        [Fact]
        public void GetById_UserExists_ReturnsMappedUserDTO()
        {
            var mapper = CreateMapper();

            var expectedUser = new User
            {
                userId = 10,
                name = "Test User",
                email = "test@example.com"
            };

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.Get(It.IsAny<int>())).Returns(expectedUser);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new UserService(mockUow.Object, mapper);

            var dto = service.GetById(10);

            Assert.NotNull(dto);
            Assert.Equal(expectedUser.userId, dto.UserId);
            Assert.Equal(expectedUser.name, dto.Name);
            Assert.Equal(expectedUser.email, dto.Email);
        }

        [Fact]
        public void Authenticate_ValidCredentials_ReturnsUserDTO()
        {
            var mapper = CreateMapper();

            var mockUser = new Mock<User>() { CallBase = true };
            mockUser.Object.userId = 5;
            mockUser.Object.name = "Auth User";
            mockUser.Object.email = "auth@example.com";
            mockUser.Setup(u => u.loginUser("login1", "pass1")).Returns(true);

            var usersList = new List<User> { mockUser.Object };

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(usersList);

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new UserService(mockUow.Object, mapper);

            var result = service.Authenticate("login1", "pass1");

            Assert.NotNull(result);
            Assert.Equal(5, result.UserId);
            Assert.Equal("Auth User", result.Name);
            Assert.Equal("auth@example.com", result.Email);
        }

        [Fact]
        public void Authenticate_InvalidCredentials_ReturnsNull()
        {
            var mapper = CreateMapper();

            var mockUser = new Mock<User>() { CallBase = true };
            mockUser.Object.userId = 7;
            mockUser.Setup(u => u.loginUser("wrong", "creds")).Returns(false);

            var mockUserRepo = new Mock<IUserRepository>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User> { mockUser.Object });

            var mockUow = new Mock<IUnitOfWork>();
            mockUow.Setup(u => u.Users).Returns(mockUserRepo.Object);

            var service = new UserService(mockUow.Object, mapper);

            var result = service.Authenticate("wrong", "creds");

            Assert.Null(result);
        }
    }
}