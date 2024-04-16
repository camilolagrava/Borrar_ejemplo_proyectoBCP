using Borrar_BCP_CL_2.Context;
using Borrar_BCP_CL_2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Borrar_BCP_CL_2.Models.ost;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Borrar_BCP_CL_2.Models.Dto;

namespace Borrar_BCP_CL_2.Services
{
    public class ContratoService
    {
        private readonly BcpCl2Context _context;

        public ContratoService(BcpCl2Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contrato>> GetAllContrato()
        {
            return await _context.Contratos.ToListAsync();
        }

        public async Task<Contrato> GetContratoById(int id)
        {
            var contrato = await _context.Contratos.FindAsync(id);

            return contrato;
        }

        public async Task<int> EditContratoById(int id, Contrato contrato)
        {
            if (id != contrato.ContratoId)
            {
                return 0;
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
                    return 1;
                }
                else
                {
                    throw;
                }
            }

            return 2;
        }

        private bool ContratoExists(int id)
        {
            return _context.Contratos.Any(e => e.ContratoId == id);
        }

        public async Task<Contrato> CreateUsuario(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            return contrato;
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

        public async Task<Respuesta> RespuestaEspecifica(Contrato contrato, Cliente cliente)
        {
            Respuesta respuesta = new Respuesta();

            respuesta.ContratoID = $"{contrato.ContratoId}";
            respuesta.Fecha = contrato.FechaFinal;
            respuesta.RepresentanteLegal = cliente.Nombres + " " + cliente.Paterno + " " + cliente.Materno;
            respuesta.NombreProvedor = contrato.NombresProvedor + " " + contrato.PaternoProvedor;
            respuesta.DocumentoProvedor = contrato.DocumentoProvedor;
            respuesta.Domicilio = contrato.Domicilio;
            respuesta.Direccion = contrato.DireccionAmbiente;
            respuesta.Ciudad = contrato.Ciudad;
            respuesta.Superficie = contrato.Superficie;
            respuesta.Importe = contrato.Importe;
            respuesta.Cuenta = contrato.Cuenta;

            return respuesta;
        }

        public async Task<Respuesta_Conteo> ListaRespuestas(IEnumerable<Contrato> list)
        {
            List<Respuesta> preList = new List<Respuesta>();
            List<int> inexistente = new List<int>();
            Respuesta_Conteo r_c = new Respuesta_Conteo();
            
            foreach (Contrato contrato in list)
            {
                var cliente = await _context.Clientes.FindAsync(contrato.ClienteId);

                if (cliente == null)
                {
                    inexistente.Add((int)contrato.ClienteId);
                }
                else
                {
                    preList.Add(await RespuestaEspecifica(contrato, cliente));
                }
                
            }
            r_c.Respuestas = preList;
            r_c.cuantity = preList.Count;
            r_c.ClientesInexistentes = inexistente;
            return r_c;
        }

        public async Task<IEnumerable<Contrato>> GetByDate(DateOnly date)
        {
            var contratos = await _context.Contratos.Where(c => c.FechaFinal == date).ToListAsync();
            return contratos;
        }

    }
}

