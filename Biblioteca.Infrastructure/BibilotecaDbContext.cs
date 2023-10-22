using Biblioteca.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure;

public class BibilotecaDbContext : IdentityDbContext
{
    public BibilotecaDbContext (DbContextOptions<BibilotecaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

    }


    public DbSet<Libro> Libros { get; set; }
    public DbSet<Autor> Autores { get; set; }
}