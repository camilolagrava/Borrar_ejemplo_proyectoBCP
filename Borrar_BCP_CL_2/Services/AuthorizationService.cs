using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Borrar_BCP_CL_2.Services.Interface;
using Borrar_BCP_CL_2.Models.Dto;
using Borrar_BCP_CL_2.Models.ost;
using Borrar_BCP_CL_2.Context;
using System.IdentityModel.Tokens.Jwt;
using Borrar_BCP_CL_2.Models;


namespace Borrar_BCP_CL_2.Services
{
    public class AuthorizationService : IAutorizationService
    {
        private readonly IConfiguration _configuration;
        public AuthorizationService( IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(string id)
        {
            var key = _configuration.GetValue<string>("JwtSetting:key");
            var keyBites = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(
                new Claim(ClaimTypes.NameIdentifier, id)
                );

            var tokenCredential = new SigningCredentials(
                new SymmetricSecurityKey(keyBites),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = tokenCredential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;

        }

        public async Task<AuthorizationResponse> GiveToken( Usuario user)
        {
            if (user == null)
            {
                return await Task.FromResult<AuthorizationResponse>(null);
            }
            
            string token = GenerateToken(user.UsuarioId.ToString());

            return new AuthorizationResponse()
            {
                Token = token,
                Result = true,
                Msg = "OK"
            };
        }

    }
}
