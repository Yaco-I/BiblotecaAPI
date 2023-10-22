using Biblioteca.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Migrations
{
    public class SeedData
    {
        public static async Task Initialize(BibilotecaDbContext context, UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var admin = new IdentityUser
                {
                    UserName = "admin@test.com",
                    Email = "admin@test.com",
                };
                await userManager.CreateAsync(admin, "Pa$$w0rd123");
                await userManager.AddClaimAsync(admin, new Claim("esAdmin", "1"));

                var usuario = new IdentityUser
                {
                    UserName = "usuario@test.com",
                    Email = "usuario@test.com"
                };
                await userManager.CreateAsync(usuario, "Pa$$w0rd123");
                await userManager.AddClaimAsync(usuario, new Claim("EsEmpleado", "1"));

                var cliente = new IdentityUser
                {
                    UserName = "usuario@test.com",
                    Email = "cliente@test.com"
                };
                await userManager.CreateAsync(cliente, "Pa$$w0rd123");
                await userManager.AddClaimAsync(cliente, new Claim("esCliente", "1"));


                await context.SaveChangesAsync();
            }


            if (!context.Autores.Any())
            {
                List<Autor> Autores = new List<Autor>()
                {
                    new Autor
                            {
                                
                                Nombre = "J.R.R.",
                                Seudonimo = "J.R.R.",
                                FechaNacimiento = new DateTime(1892, 1, 3),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
                            },
                            new Autor
                            {
                                
                                Nombre = "Agatha Christie",
                                Seudonimo = "Agatha Christie",
                                FechaNacimiento = new DateTime(1890, 9, 15),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
                            },
                            new Autor
                            {
                                
                                Nombre = "George Orwell",
                                Seudonimo = "George Orwell",
                                FechaNacimiento = new DateTime(1903, 6, 25),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
                            },new Autor
    {
        
        Nombre = "Jane Austen",
        Seudonimo = "Jane Austen",
        FechaNacimiento = new DateTime(1775, 12, 16),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Autor
    {
        
        Nombre = "F. Scott Fitzgerald",
        Seudonimo = "F. Scott Fitzgerald",
        FechaNacimiento = new DateTime(1896, 9, 24),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Autor
    {
        
        Nombre = "Leo Tolstoy",
        Seudonimo = "Leo Tolstoy",
        FechaNacimiento = new DateTime(1828, 9, 9),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Autor
    {
        
        Nombre = "Mark Twain",
        Seudonimo = "Mark Twain",
        FechaNacimiento = new DateTime(1835, 11, 30),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Autor
    {
        
        Nombre = "Charles Dickens",
        Seudonimo = "Charles Dickens",
        FechaNacimiento = new DateTime(1812, 2, 7),
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    }
                };
                await context.AddRangeAsync(Autores);
                try
                {

                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            if (!context.Libros.Any())
            {
                List<Libro> libros = new List<Libro>()
                {

                new Libro
    {
        Titulo = "1984",
        Resumen = "1984 es una novela de ficción política y distopía escrita por George Orwell. La historia está ambientada en un estado totalitario donde el gobierno controla todos los aspectos de la vida de los ciudadanos. La novela sigue la vida de Winston Smith, un hombre que trabaja en el Ministerio de la Verdad y lucha contra la opresión del partido gobernante.",
        FechaPublicacion = new DateTime(1949, 6, 8),
        AutorId = 3,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now


    },
    new Libro
    {
        Titulo = "Orgullo y prejuicio",
        Resumen = "Orgullo y prejuicio es una novela de la escritora británica Jane Austen. La historia gira en torno a la vida de la joven Elizabeth Bennet y su relación con el apuesto pero orgulloso Sr. Darcy. La novela es una sátira social que aborda temas de clase, matrimonio y prejuicios.",
        FechaPublicacion = new DateTime(1813, 1, 28),
        AutorId = 4,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Libro
    {
        Titulo = "El Gran Gatsby",
        Resumen = "El Gran Gatsby es una novela escrita por F. Scott Fitzgerald. La historia se desarrolla en la década de 1920 y sigue la vida de Jay Gatsby, un misterioso millonario obsesionado con recuperar a su amor de la juventud, Daisy Buchanan. La novela trata temas de riqueza, decadencia y el Sueño Americano.",
        FechaPublicacion = new DateTime(1925, 4, 10),
        AutorId = 5,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Libro
    {
        Titulo = "Guerra y paz",
        Resumen = "Guerra y paz es una novela épica escrita por el autor ruso León Tolstoy. La obra narra la vida de cinco familias aristocráticas rusas durante la invasión napoleónica de Rusia en el siglo XIX. La novela es conocida por su amplitud temática y sus personajes complejos.",
        FechaPublicacion = new DateTime(1869, 1, 1),
        AutorId = 6,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Libro
    {
        Titulo = "Las aventuras de Tom Sawyer",
        Resumen = "Las aventuras de Tom Sawyer es una novela escrita por Mark Twain. La historia sigue las travesuras del niño Tom Sawyer en una pequeña ciudad a orillas del río Misisipi. La novela es un clásico de la literatura infantil y aborda temas de libertad y aventura.",
        FechaPublicacion = new DateTime(1876, 12, 1),
        AutorId = 7,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    },
    new Libro
    {
        Titulo = "Cuento de Navidad",
        Resumen = "Cuento de Navidad es una novela corta escrita por Charles Dickens. La historia sigue a Ebenezer Scrooge, un hombre avaro y egoísta que es visitado por tres espíritus en Nochebuena. Los espíritus le muestran la importancia de la generosidad y la compasión.",
        FechaPublicacion = new DateTime(1843, 12, 19),
        AutorId = 8,
        CreatedBy = "AdminTest",
        CreatedDate = DateTime.Now,
        LastModifiedBy = "AdminTest",
        LastModifiedDate = DateTime.Now
    }
                };
                await context.AddRangeAsync(libros);
                try
                {

                    await context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }



        }

    }
}
