using LH_PET.Models;
using Microsoft.EntityFrameworkCore;

namespace LH_PET.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Fornecedor> Fornecedores { get; set; } = null!;

        public DbSet<Animal> Animais { get; set; } = null!;

    }
}
