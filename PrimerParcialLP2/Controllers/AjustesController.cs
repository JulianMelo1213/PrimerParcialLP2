using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Ajuste;

namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AjustesController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper _mapper;

        public AjustesController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Ajustes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AjusteGetDTO>>> GetAjustes()
        {
            var ajusteList = await _context.Ajustes
                .Include(a => a.Producto)
                .Include(a => a.Almacen)
                .Select(a => new AjusteGetDTO
                {
                    AjusteId = a.AjusteId,
                    ProductoId = a.ProductoId,
                    ProductoNombre = a.Producto.Nombre,
                    AlmacenId = a.AlmacenId,
                    AlmacenNombre = a.Almacen.Nombre,
                    Cantidad = a.Cantidad,
                    Fecha = a.Fecha,
                    Tipo = a.Tipo
                })
                .ToListAsync();

            return Ok(ajusteList);
        }

        // GET: api/Ajustes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AjusteGetDTO>> GetAjuste(int id)
        {
            var ajuste = await _context.Ajustes
                .Include(a => a.Producto)
                .Include(a => a.Almacen)
                .FirstOrDefaultAsync(a => a.AjusteId == id);

            if (ajuste == null)
            {
                return NotFound();
            }

            var ajusteDTO = _mapper.Map<AjusteGetDTO>(ajuste);

            return Ok(ajusteDTO);
        }

        // PUT: api/Ajustes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAjuste(int id, AjustePutDTO ajusteDto)
        {
            if (id != ajusteDto.AjusteId)
            {
                return BadRequest();
            }

            var ajuste = await _context.Ajustes.FindAsync(id);
            if (ajuste == null)
            {
                return NotFound();
            }

            _mapper.Map(ajusteDto, ajuste);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AjusteExists(id))
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

        // POST: api/Ajustes
        [HttpPost]
        public async Task<ActionResult<Ajuste>> PostAjuste(AjusteInsertDTO ajusteDto)
        {
            var ajuste = _mapper.Map<Ajuste>(ajusteDto);
            await _context.Ajustes.AddAsync(ajuste);
            await _context.SaveChangesAsync();

            return Ok(ajuste.AjusteId);
        }

        // DELETE: api/Ajustes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAjuste(int id)
        {
            var ajuste = await _context.Ajustes.FindAsync(id);
            if (ajuste == null)
            {
                return NotFound();
            }

            _context.Ajustes.Remove(ajuste);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AjusteExists(int id)
        {
            return _context.Ajustes.Any(e => e.AjusteId == id);
        }
    }
}
