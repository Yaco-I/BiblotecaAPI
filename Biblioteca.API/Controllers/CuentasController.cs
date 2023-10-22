using Biblioteca.Service.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.API.Controllers;

   
public class CuentasController : BaseController
{
    private readonly UserManager<IdentityUser> userManager;
    private readonly IConfiguration configuration;
    private readonly SignInManager<IdentityUser> signInManager;
    public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
    {
        this.userManager = userManager;
        this.configuration = configuration;
        this.signInManager = signInManager;
    }

    [HttpPost("Registrar")]
    public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
    {
        var usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };
        var resultado = await userManager.CreateAsync(usuario, credencialesUsuario.Password);
        if (resultado.Succeeded) {     
            return await ConstruirToken(credencialesUsuario);
        }
        else
        {
            return BadRequest(resultado.Errors);
        }
    }


    [HttpPost("Login")]
    public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
    {
        var resultado = await signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);
            
        if (resultado.Succeeded)
        {
            return await ConstruirToken(credencialesUsuario);
        }
        else
        {
            return BadRequest("Login incorrecto");
        }
    }


    [HttpGet("RenovarToken")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<RespuestaAutenticacion>> Renovar()
    {
        var emailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
        var credencialesUsuario = new CredencialesUsuario() { Email = emailClaim };
        return await ConstruirToken(credencialesUsuario);
    }

    [HttpPost("HacerAdmin")]
    public async Task<ActionResult> HacerAdmin(AgregarClaims agregarClaims) 
    {
        var usuario = await userManager.FindByEmailAsync(agregarClaims.Email);
        await userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
        return NoContent();
    }
    [HttpPost("RemoverAdmin")]
    public async Task<ActionResult> RemoverAdmin(AgregarClaims agregarClaims)
    {
        var usuario = await userManager.FindByEmailAsync(agregarClaims.Email);
        await userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
        return NoContent();
    }

    private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
    {
        var claims = new List<Claim>() { new Claim("email", credencialesUsuario.Email) };


        var usuario = await userManager.FindByEmailAsync(credencialesUsuario.Email);
        var claimsDB = await userManager.GetClaimsAsync(usuario); 
        claims.AddRange(claimsDB);

        var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt")["key"]));
        var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

        var expiracion = DateTime.UtcNow.AddMinutes(60);

        var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiracion, signingCredentials: creds);


        return new RespuestaAutenticacion()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
            Expiracion = expiracion
        };
    }
}
