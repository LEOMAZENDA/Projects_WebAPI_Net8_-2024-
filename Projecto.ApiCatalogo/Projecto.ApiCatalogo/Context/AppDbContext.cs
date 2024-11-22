using Microsoft.EntityFrameworkCore;
using Projecto.ApiCatalogo.Models;

namespace Projecto.ApiCatalogo.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    
    public DbSet<Categoria>? Categorias { get; set; }
    public DbSet<Producto>? Productos { get; set; }
}