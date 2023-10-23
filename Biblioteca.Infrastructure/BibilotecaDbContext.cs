using Biblioteca.Infrastructure.Models;
using Biblioteca.Infrastructure.Views;
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

        builder.Entity<LibroConAutorView>().HasNoKey().ToView("LibroConAutor");
        builder.Entity<LibroConAutorFilter>().HasNoKey().ToView("LibroConAutorGetByFilter");
    }


    public DbSet<Libro> Libros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<LibroConAutorView> LibroConAutor { get; set; }
}