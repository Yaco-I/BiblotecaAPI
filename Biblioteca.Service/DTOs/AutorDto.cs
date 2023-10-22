using Biblioteca.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.DTOs
{
    public class AutorDto 
    {     
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        public string Seudonimo { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public List<LibroDto>? Libros { get; set; }
    }
}
