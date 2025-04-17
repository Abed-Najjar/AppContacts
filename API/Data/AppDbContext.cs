using API.Entities;
using Microsoft.EntityFrameworkCore;
namespace API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }
    public DbSet<Contacts> Contacts {get; set;}
    public DbSet<AppUser> Users {get; set;}
}
