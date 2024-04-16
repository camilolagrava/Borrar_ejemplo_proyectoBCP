using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Borrar_BCP_CL_2.Context;
using Borrar_BCP_CL_2.Models;
using Borrar_BCP_CL_2.Models.ost;
using Borrar_BCP_CL_2.Services;
using Microsoft.AspNetCore.Authorization;
using Borrar_BCP_CL_2.Services.Interface;


using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using Borrar_BCP_CL_2.Models.Dto;

namespace Borrar_BCP_CL_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly BcpCl2Context _context;
        private readonly UsuarioService _usuarioService;
        private readonly AuthorizationService _autorizationService;
        private readonly IEmailService _emailService;

        public UsuariosController(BcpCl2Context context,IConfiguration _configuration, IEmailService emailService)
        {
            _context = context;
            _autorizationService = new AuthorizationService(_configuration);
            _usuarioService = new UsuarioService(context, _autorizationService);   
            _emailService = emailService;
            //_generatePdf = generatePdf;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.UsuarioId)
            {
                return BadRequest();
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
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.UsuarioId }, usuario);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.UsuarioId == id);
        }

        [HttpPost("FormularioRegistro")]
        public async Task<IActionResult> FormularioRegistro(Register formulario)
        {
            return Ok(await _usuarioService.Register(formulario));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login formulario)
        {
            var t = await _usuarioService.Login(formulario);
            if (t == null)
            {
                return Unauthorized();
            }

            return Ok(t);
        }

        [Authorize]
        [HttpGet("AllUsers")]
        public async Task<ActionResult<IEnumerable<Usuario>>> AllUser()
        {
            return await _usuarioService.GetAllUsuarios();
        }

        [HttpGet("Pdf")]
        public async Task<ActionResult> pdf()
        {
            var document = new PdfDocument();
            string html = "<h1> Que se jodan, porfavor que funcione </h1>";
            PdfGenerator.AddPdfPages(document, html, PageSize.A4 );
            byte[]? response = null;
            using (MemoryStream ms = new MemoryStream()) {
                document.Save(ms);
                response = ms.ToArray();
            }

            string filename = "file.pdf";
            return File(response, "application/pdf", filename);

        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail(EmailDTO request)
        {
            _emailService.SendEmail(request);
            return Ok();
        }



    }
}
