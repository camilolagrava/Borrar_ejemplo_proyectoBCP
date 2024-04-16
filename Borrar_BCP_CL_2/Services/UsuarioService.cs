using Borrar_BCP_CL_2.Context;
using Borrar_BCP_CL_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Borrar_BCP_CL_2.Models.ost;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Borrar_BCP_CL_2.Models.Dto;

namespace Borrar_BCP_CL_2.Services
{
    public class UsuarioService
    {
        private readonly BcpCl2Context _context;
        private readonly AuthorizationService _authorizationService;

        public UsuarioService(BcpCl2Context context, AuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            return usuario;
        }

        public async Task<int> EditUsuarioById(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return 0;
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
                {
                    return 1;
                }
                else
                {
                    throw;
                }
            }

            return 2;
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }

        public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<int> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return 0;
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return 1;
        }

        public async Task<int> Register(Register formulario)
        {
            if(!ExistUsuarioByEmail(formulario.email))
            {
                _context.Usuarios.Add(
                    new Usuario { 
                        Email = formulario.email,
                        Contrasenna = formulario.pass,
                        Nombre = formulario.user
                    }
                    );
                await _context.SaveChangesAsync();
                return 0;
            }
            return 1;
        }

        private Boolean ExistUsuarioByEmail(string email)
        {
            return _context.Usuarios.Any(e => e.Email == email);
            
        }

        private Boolean ValidationUserPass(string email, string pass)
        {
            return _context.Usuarios.Any(u => u.Email == email && u.Contrasenna == pass);
        }

        private async Task<Usuario> UserPassLogin(string email, string pass)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Contrasenna == pass);
        }

        public async Task<AuthorizationResponse> Login(Login formulario)
        {
            var user = await UserPassLogin(formulario.email, formulario.pass);
        if (user != null)
        {
                var res = await _authorizationService.GiveToken(user);
                return res;
        }

            return null;
        }



    }
}
