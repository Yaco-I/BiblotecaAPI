using Biblioteca.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Views
{
    public class LibroConAutorView
    {
        public int  Id{ get; set; }
        public string Titulo { get; set; }
        public string Resumen { get; set; }
        public DateTime FechaPublicacion { get; set; }
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Seudonimo { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
