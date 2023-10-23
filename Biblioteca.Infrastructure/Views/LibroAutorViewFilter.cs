using Biblioteca.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Biblioteca.Infrastructure.Views
{
    public class LibroAutorViewFilter
    {
		public int? LibroId { get; set; }
		public string? Titulo { get; set; }

		public string? Resumen { get; set; }
		public int? AutorId { get; set; }
		public string? Nombre { get; set; }
		public string? Seudonimo { get; set; }
		public int? PageNumber { get; set; } = 1;
		public int? PageSize { get; set; } = 100;
    }
}
