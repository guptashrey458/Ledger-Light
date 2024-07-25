using Microsoft.EntityFrameworkCore;
using LedgerLight;
namespace LedgerLight.Models
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
    }
}
