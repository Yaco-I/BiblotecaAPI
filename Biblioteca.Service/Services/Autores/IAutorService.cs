using Biblioteca.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.Services.Autores
{
    public interface IAutorService
    {
        Task<int> InsertAsync(AutorDto autorDto);
        Task<int?> UpdateAsync(AutorDto autorDto);
        Task<bool> DeleteAsync(int id);
        Task<AutorDto> GetByIdAsync(int id);
        Task<IEnumerable<AutorDto>> GetAllAsync();
    }
}
