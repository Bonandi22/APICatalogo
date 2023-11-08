using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;
namespace APICatalogo.Context;

public class AppDbContext: DbContext
{ 
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

  public DbSet<Category>? Categorias { get; set; }
  public DbSet<Product>? Produtos { get; set; }


    
}
