using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Borrar_BCP_CL_2.Context;
using Borrar_BCP_CL_2.Models;
using Borrar_BCP_CL_2.Services;

namespace Borrar_BCP_CL_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratosController : ControllerBase
    {
        private readonly BcpCl2Context _context;
        private readonly ContratoService _contratoService;

        public ContratosController(BcpCl2Context context)
        {
            _context = context; 
            _contratoService = new ContratoService(context);
        }

        // GET: api/Contratoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contrato>>> GetContratos()
        {
            return await _context.Contratos.ToListAsync();
        }

        // GET: api/Contratoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> GetContrato(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            if (contrato == null)
            {
                return NotFound();
            }

            return contrato;
        }

        // PUT: api/Contratoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContrato(int id, Contrato contrato)
        {
            if (id != contrato.ContratoId)
            {
                return BadRequest();
            }

            _context.Entry(contrato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContratoExists(id))
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

        // POST: api/Contratoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contrato>> PostContrato(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContrato", new { id = contrato.ContratoId }, contrato);
        }

        // DELETE: api/Contratoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);
            if (contrato == null)
            {
                return NotFound();
            }

            _context.Contratos.Remove(contrato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.ContratoId == id);
        }

        [HttpGet("GetContratosByDate/{date}")]
        public async Task<IActionResult> GetContratosByDate(DateOnly date)
        {
            var list = await _contratoService.GetByDate(date);

            var anw = await _contratoService.ListaRespuestas(list);

            return Ok(anw);
        }
    }
}
