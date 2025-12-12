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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public UserDTO GetById(int id)
        {
            var user = _uow.Users.Get(id);
            if (user == null) return null;
            return _mapper.Map<User, UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _uow.Users.GetAll();
            return _mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
        }

        public UserDTO Authenticate(string login, string password)
        {
            var candidates = _uow.Users.GetAll();
            var user = candidates.FirstOrDefault(u => u.loginUser(login, password));
            if (user == null) return null;
            return _mapper.Map<User, UserDTO>(user);
        }
    }
}