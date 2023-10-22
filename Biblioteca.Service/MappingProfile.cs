using AutoMapper;
using Biblioteca.Infrastructure.Models;
using Biblioteca.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Libro
            CreateMap<Libro, LibroDto>();
            CreateMap<LibroDto, Libro>();
            #endregion Libro


            #region Autor
            CreateMap<Autor, AutorDto>();
            CreateMap<AutorDto, Autor>();
            #endregion Autor
            

        }
    }
}
