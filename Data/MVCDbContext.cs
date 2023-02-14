using Microsoft.EntityFrameworkCore;
using WEB2.Models.Domein;

namespace WEB2.Data;

public class MVCDbContext : DbContext
{
    public MVCDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    
}