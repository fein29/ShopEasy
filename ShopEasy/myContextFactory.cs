using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ShopEasy.Models;

namespace ShopEasy
{
    public class myContextFactory : IDesignTimeDbContextFactory<myContext>
    {
        public myContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<myContext>();
            optionsBuilder.UseSqlServer("server=SHOURYASANYAL\\SQLEXPRESS; database=Shopeasy; Integrated Security=true; TrustServerCertificate=true");
            return new myContext(optionsBuilder.Options);
        }
    }
}
