using Biblioteca.Service.DTOs;
using Biblioteca.Service.Services.Autores;
using Biblioteca.Service.Services.Libros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.API.Controllers
{
    public class AutorController : BaseController
    {
        private readonly IAutorService _autorService;
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;   
        }

        #region Queries
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AdminOrClienteClaim)]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetLibrosAsync()
        {
            try
            {
                return Ok(await _autorService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AllClaims)]
        public async Task<ActionResult<AutorDto>> GetByIdAsync(int id)
        {
            if(id <= 0)
            {
                return BadRequest("El id no puede ser menor 0");
            }
            try
            {
                var result = await _autorService.GetByIdAsync(id);
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
        public async Task<ActionResult<int>> InsertAsync(AutorDto autorDto)
        {
            try
            {
                return Ok(_autorService.InsertAsync(autorDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = ApplicationConstants.AdminOrEmpleadoClaim)]
        public async Task<ActionResult<int>> UpdateAsync(AutorDto autorDto)
        {
            try
            {
                var result = await _autorService.UpdateAsync(autorDto);
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
                if(await _autorService.DeleteAsync(id))
                {
                    return Ok();
                }else
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
