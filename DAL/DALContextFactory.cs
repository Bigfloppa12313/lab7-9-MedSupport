using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Catalog.DAL
{
    public class DALContextFactory : IDesignTimeDbContextFactory<DALContext>
    {
        public DALContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DALContext>();

            var conn = "Server=(localdb)\\mssqllocaldb;Database=MedSupportDb;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(conn);

            return new DALContext(optionsBuilder.Options);
        }
    }
}