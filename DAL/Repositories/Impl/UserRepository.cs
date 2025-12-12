using Catalog.DAL.EF;
using Catalog.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.DAL.Repositories.Impl
{
    public class UserRepository
        : BaseRepository<Entities.User>, IUserRepository
    {
        internal UserRepository(DALContext context)
            : base(context)
        {
        }
    }
}
