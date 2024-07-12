using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Entrada;

namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradumsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper _mapper;

        public EntradumsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Entradums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntradaGetDTO>>> GetEntrada()
        {
            var entradaList = await _context.Entrada
                .Include(e => e.Producto)
                .Select(e => new EntradaGetDTO
                {
                    EntradaId = e.EntradaId,
                    ProductoId = e.ProductoId,
                    ProductoNombre = e.Producto.Nombre, // Nuevo campo
                    Cantidad = e.Cantidad,
                    Fecha = e.Fecha
                })
                .ToListAsync();

            return Ok(entradaList);
        }

        // GET: api/Entradums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntradaGetDTO>> GetEntradum(int id)
        {
            var entrada = await _context.Entrada
                .Include(e => e.Producto)
                .Where(e => e.EntradaId == id)
                .Select(e => new EntradaGetDTO
                {
                    EntradaId = e.EntradaId,
                    ProductoId = e.ProductoId,
                    ProductoNombre = e.Producto.Nombre, // Nuevo campo
                    Cantidad = e.Cantidad,
                    Fecha = e.Fecha
                })
                .FirstOrDefaultAsync();

            if (entrada == null)
            {
                return NotFound();
            }

            return Ok(entrada);
        }

        // PUT: api/Entradums/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradum(int id, EntradaPutDTO entradaDTO)
        {
            if (id != entradaDTO.EntradaId)
            {
                return BadRequest();
            }

            var entrada = await _context.Entrada.FindAsync(id);
            if (entrada == null)
            {
                return NotFound();
            }

            _mapper.Map(entradaDTO, entrada);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntradumExists(id))
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

        // POST: api/Entradums
        [HttpPost]
        public async Task<ActionResult<Entradum>> PostEntradum(EntradaInsertDTO entradaDto)
        {
            var entrada = _mapper.Map<Entradum>(entradaDto);
            await _context.Entrada.AddAsync(entrada);
            await _context.SaveChangesAsync();

            return Ok(entrada.EntradaId);
        }

        // DELETE: api/Entradums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntradum(int id)
        {
            var entradum = await _context.Entrada.FindAsync(id);
            if (entradum == null)
            {
                return NotFound();
            }

            _context.Entrada.Remove(entradum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntradumExists(int id)
        {
            return _context.Entrada.Any(e => e.EntradaId == id);
        }
    }
}
