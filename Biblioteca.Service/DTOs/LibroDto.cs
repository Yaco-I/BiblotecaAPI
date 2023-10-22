using Biblioteca.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.DTOs
{
    public class LibroDto
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [StringLength(500)]
        public string Resumen { get; set; }

        public DateTime FechaPublicacion { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

    }
}
