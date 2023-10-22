using Biblioteca.Infrastructure.Models;
using Biblioteca.Service.DTOs;
using Biblioteca.Service.Services.Libros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    public class LibroController : BaseController
    {
        public readonly ILibroService librosService;

        public LibroController(ILibroService librosService)
        {
            this.librosService = librosService;
        }


        #region Queries
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AllClaims)]
        public async Task<ActionResult<IEnumerable<LibroDto>>> GetLibrosAsync()
        {
            try
            {
                return Ok(await librosService.GetAllAsync());
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AllClaims)]
        public async Task<ActionResult<LibroDto>> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest("El id no puede ser menor 0");
            }
            try
            {
                var result = await librosService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        #endregion Queries



        #region Actions

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AdminOrEmpleadoClaim)]
        public async Task<ActionResult<int>> InsertAsync(LibroDto libro)
        {
            try
            {
                return Ok(await librosService.InsertAsync(libro));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AdminOrEmpleadoClaim)]
        public async Task<ActionResult<int>> UpdateAsync(LibroDto libroDto)
        {
            try
            {
                var result = await librosService.UpdateAsync(libroDto);
                if (result.HasValue)
                {
                    return Ok(result.Value);
                }
                else
                {
                    return NotFound("No se encontró el registro para actualizar");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AdminClaim)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                if(await librosService.DeleteAsync(id))
                {
                    return Ok();
                }
                else
                {
                    return NotFound("No se encontró el registro");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion Actions
    }
}
