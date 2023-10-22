using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.DTOs
{
    public class AgregarClaims
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        //public List<ClaimDto> Claims { get; set; }
    }
}
