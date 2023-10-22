using AutoMapper;
using Biblioteca.Infrastructure;
using Biblioteca.Infrastructure.Models;
using Biblioteca.Service.DTOs;

namespace Biblioteca.Service.Services.Autores
{
    public class AutorService : IAutorService
    {
        private readonly BibilotecaDbContext _context;
        private readonly IRepository<Autor> _repository;
        private readonly IMapper _mapper;
        public AutorService(BibilotecaDbContext context, IRepository<Autor> repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> InsertAsync(AutorDto autorDto)
        {
            Autor autor = _mapper.Map<Autor>(autorDto);
            try
            {

                autor = await _repository.InsertAsync(autor);
                return autor.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo insertar el autor", ex);
            }
        }

        public async Task<int?> UpdateAsync(AutorDto autorDto)
        {
            Autor autor = _mapper.Map<Autor>(autorDto);
            try
            {
                var result = await _repository.UpdateAsync(autor);

                return result?.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("No se pudo actualizar el Autor", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<AutorDto> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    throw new Exception("No se encontró el registro");
                }
                return _mapper.Map<AutorDto>(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el registro", ex);
            }
        }

        public async Task<IEnumerable<AutorDto>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AutorDto>>(entities);
        }




    }
}
