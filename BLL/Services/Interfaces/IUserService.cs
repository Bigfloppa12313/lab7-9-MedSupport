using System.Collections.Generic;
using Catalog.BLL.DTOs;

namespace Catalog.BLL.Services.Interfaces
{
    public interface IUserService
    {
        UserDTO GetById(int id);
        IEnumerable<UserDTO> GetAll();
        UserDTO Authenticate(string login, string password);
    }
}