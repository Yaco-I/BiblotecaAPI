using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Infrastructure.Models
{
    public class Libro : IEntity, IAuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        [StringLength(500)]
        public string Resumen { get; set; }

        public DateTime FechaPublicacion { get; set; }
        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public  DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }


    }
}
