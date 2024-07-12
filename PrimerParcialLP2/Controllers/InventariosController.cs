using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Inventario;

namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventariosController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper _mapper;

        public InventariosController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Inventarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioGetDTO>>> GetInventarios()
        {
            var inventarioList = await _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Almacen)
                .Select(i => new InventarioGetDTO
                {
                    InventarioId = i.InventarioId,
                    ProductoId = i.ProductoId,
                    ProductoNombre = i.Producto.Nombre, // Nuevo campo
                    AlmacenId = i.AlmacenId,
                    AlmacenNombre = i.Almacen.Nombre, // Nuevo campo
                    Cantidad = i.Cantidad,
                    Fecha = i.Fecha
                })
                .ToListAsync();

            return Ok(inventarioList);
        }

        // GET: api/Inventarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventarioGetDTO>> GetInventario(int id)
        {
            var inventario = await _context.Inventarios
                .Include(i => i.Producto)
                .Include(i => i.Almacen)
                .Where(i => i.InventarioId == id)
                .Select(i => new InventarioGetDTO
                {
                    InventarioId = i.InventarioId,
                    ProductoId = i.ProductoId,
                    ProductoNombre = i.Producto.Nombre, // Nuevo campo
                    AlmacenId = i.AlmacenId,
                    AlmacenNombre = i.Almacen.Nombre, // Nuevo campo
                    Cantidad = i.Cantidad,
                    Fecha = i.Fecha
                })
                .FirstOrDefaultAsync();

            if (inventario == null)
            {
                return NotFound();
            }

            return Ok(inventario);
        }

        // PUT: api/Inventarios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventario(int id, InventarioPutDTO inventarioDto)
        {
            if (id != inventarioDto.InventarioId)
            {
                return BadRequest();
            }

            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _mapper.Map(inventarioDto, inventario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventarioExists(id))
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

        // POST: api/Inventarios
        [HttpPost]
        public async Task<ActionResult<Inventario>> PostInventario(InventarioInsertDTO inventarioDto)
        {
            var inventario = _mapper.Map<Inventario>(inventarioDto);
            await _context.Inventarios.AddAsync(inventario);
            await _context.SaveChangesAsync();

            return Ok(inventario.InventarioId);
        }

        // DELETE: api/Inventarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventario(int id)
        {
            var inventario = await _context.Inventarios.FindAsync(id);
            if (inventario == null)
            {
                return NotFound();
            }

            _context.Inventarios.Remove(inventario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventarioExists(int id)
        {
            return _context.Inventarios.Any(e => e.InventarioId == id);
        }
    }
}
