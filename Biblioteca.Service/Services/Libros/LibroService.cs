using AutoMapper;
using Biblioteca.Infrastructure;
using Biblioteca.Infrastructure.Models;
using Biblioteca.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service.Services.Libros;

public class LibrosService : ILibroService
{
    private readonly BibilotecaDbContext _context;
    private readonly IRepository<Libro> _repository;
    private readonly IMapper _mapper;

    public LibrosService(BibilotecaDbContext context, IRepository<Libro> repository, IMapper mapper)
    {
        _context = context;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> InsertAsync(LibroDto libroDto)
    {
        Libro libro = _mapper.Map<Libro>(libroDto);
        try
        {
            libro = await _repository.InsertAsync(libro);
            return libro.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo insertar el libro", ex);
        }
    }

    public async Task<int?> UpdateAsync(LibroDto libroDto)
    {
        Libro libro = _mapper.Map<Libro>(libroDto);
        try
        {
            var result = await _repository.UpdateAsync(libro);

            return result?.Id;
        }
        catch (Exception ex)
        {
            throw new Exception("No se pudo actualizar el libro", ex);
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<LibroDto> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception("No se encontró el registro");
            }
            return _mapper.Map<LibroDto>(entity);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al obtener el registro", ex);
        }
    }

    public async Task<IEnumerable<LibroDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<LibroDto>>(entities);
    }



}
