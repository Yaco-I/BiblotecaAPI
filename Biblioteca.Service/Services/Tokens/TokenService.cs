using Biblioteca.Service.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Biblioteca.Service.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;

        }


        public async Task<RespuestaAutenticacion> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Email, Email = credencialesUsuario.Email };
            var resultado = await _userManager.CreateAsync(usuario, credencialesUsuario.Password);
            
            return await ConstruirToken(credencialesUsuario);

            //TODO: MIRAR 
        }

        public async Task<RespuestaAutenticacion> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await _signInManager.PasswordSignInAsync(credencialesUsuario.Email, credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credencialesUsuario);
            }
            else
            {
                throw new Exception("Login incorrecto");
            }
        }

        public async Task<RespuestaAutenticacion> Renovar()
        {
            var emailClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var credencialesUsuario = new CredencialesUsuario() { Email = emailClaim };
            return await ConstruirToken(credencialesUsuario);
        }

        public async Task HacerAdmin(AgregarClaims agregarClaims)
        {
            var usuario = await _userManager.FindByEmailAsync(agregarClaims.Email);
            await _userManager.AddClaimAsync(usuario, new Claim("esAdmin", "1"));
            //return NoContent();
        }


        public async Task RemoverAdmin(AgregarClaims agregarClaims)
        {
            var usuario = await _userManager.FindByEmailAsync(agregarClaims.Email);
            await _userManager.RemoveClaimAsync(usuario, new Claim("esAdmin", "1"));
            //return NoContent();
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credencialesUsuario)
        {
            var claims = new List<Claim>() { new Claim("email", credencialesUsuario.Email) };


            var usuario = await _userManager.FindByEmailAsync(credencialesUsuario.Email);
            var claimsDB = await _userManager.GetClaimsAsync(usuario);
            claims.AddRange(claimsDB);

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["key"]));
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
}
