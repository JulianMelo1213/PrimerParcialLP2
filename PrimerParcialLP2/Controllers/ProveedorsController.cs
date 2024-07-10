using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Proveedor;


namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper mapper;


        public ProveedorsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Proveedors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorGetDTO>>> GetProveedors()
        {
            var proveedorList = await _context.Proveedors.ToListAsync();
            var proveedorDto = mapper.Map<IEnumerable<ProveedorGetDTO>>(proveedorList);
            return Ok(proveedorDto);
        }

        // GET: api/Proveedors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorGetDTO>> GetProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);

            if (proveedor == null)
            {
                return NotFound();
            }

            var proveedorDto = mapper.Map<ProveedorGetDTO>(proveedor);

            return Ok(proveedorDto);
        }

        // PUT: api/Proveedors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, ProveedorPutDTO proveedorDto)
        {
            if (id != proveedorDto.ProveedorId)
            {
                return BadRequest();
            }

            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            mapper.Map(proveedorDto, proveedor);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
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

        // POST: api/Proveedors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(ProveedorInsertDTO proveedorDto)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorDto);
            await _context.Proveedors.AddAsync(proveedor);
            await _context.SaveChangesAsync();

            return Ok(proveedor.ProveedorId);
        }

        // DELETE: api/Proveedors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            var proveedor = await _context.Proveedors.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            _context.Proveedors.Remove(proveedor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return _context.Proveedors.Any(e => e.ProveedorId == id);
        }
    }
}
