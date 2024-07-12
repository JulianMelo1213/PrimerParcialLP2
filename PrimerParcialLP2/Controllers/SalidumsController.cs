using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Salida;

namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidumsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper _mapper;

        public SalidumsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Salidums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaGetDTO>>> GetSalidas()
        {
            var salidaList = await _context.Salida
                .Include(s => s.Producto)
                .Select(s => new SalidaGetDTO
                {
                    SalidaId = s.SalidaId,
                    ProductoId = s.ProductoId,
                    ProductoNombre = s.Producto.Nombre, // Nuevo campo
                    Cantidad = s.Cantidad,
                    Fecha = s.Fecha
                })
                .ToListAsync();

            return Ok(salidaList);
        }

        // GET: api/Salidums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaGetDTO>> GetSalida(int id)
        {
            var salida = await _context.Salida
                .Include(s => s.Producto)
                .Where(s => s.SalidaId == id)
                .Select(s => new SalidaGetDTO
                {
                    SalidaId = s.SalidaId,
                    ProductoId = s.ProductoId,
                    ProductoNombre = s.Producto.Nombre, // Nuevo campo
                    Cantidad = s.Cantidad,
                    Fecha = s.Fecha
                })
                .FirstOrDefaultAsync();

            if (salida == null)
            {
                return NotFound();
            }

            return Ok(salida);
        }

        // PUT: api/Salidums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalida(int id, SalidaPutDTO salidaDto)
        {
            if (id != salidaDto.SalidaId)
            {
                return BadRequest();
            }

            var salida = await _context.Salida.FindAsync(id);
            if (salida == null)
            {
                return NotFound();
            }

            _mapper.Map(salidaDto, salida);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidaExists(id))
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

        // POST: api/Salidums
        [HttpPost]
        public async Task<ActionResult<Salidum>> PostSalida(SalidaInsertDTO salidaDto)
        {
            var salida = _mapper.Map<Salidum>(salidaDto);
            await _context.Salida.AddAsync(salida);
            await _context.SaveChangesAsync();

            return Ok(salida.SalidaId);
        }

        // DELETE: api/Salidums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalida(int id)
        {
            var salida = await _context.Salida.FindAsync(id);
            if (salida == null)
            {
                return NotFound();
            }

            _context.Salida.Remove(salida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalidaExists(int id)
        {
            return _context.Salida.Any(e => e.SalidaId == id);
        }
    }
}
