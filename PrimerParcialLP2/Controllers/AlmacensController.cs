using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using GestionInventarios.Shared.DTOs.Almacen;

namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlmacensController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper _mapper;

        public AlmacensController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Almacens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlmacenGetDTO>>> GetAlmacens()
        {
            var almacenList = await _context.Almacen.ToListAsync();
            var almacenDto = _mapper.Map<IEnumerable<AlmacenGetDTO>>(almacenList);
            return Ok(almacenDto);
        }

        // GET: api/Almacens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AlmacenGetDTO>> GetAlmacen(int id)
        {
            var almacen = await _context.Almacen.FindAsync(id);

            if (almacen == null)
            {
                return NotFound();
            }

            var almacenDto = _mapper.Map<AlmacenGetDTO>(almacen);
            return Ok(almacenDto);
        }

        // PUT: api/Almacens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlmacen(int id, AlmacenPutDTO almacenDto)
        {
            if (id != almacenDto.AlmacenId)
            {
                return BadRequest();
            }

            var almacen = await _context.Almacen.FindAsync(id);
            if (almacen == null)
            {
                return NotFound();
            }

            _mapper.Map(almacenDto, almacen);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlmacenExists(id))
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

        // POST: api/Almacens
        [HttpPost]
        public async Task<ActionResult<Almacen>> PostAlmacen(AlmacenInsertDTO almacenDto)
        {
            var almacen = _mapper.Map<Almacen>(almacenDto);
            await _context.Almacen.AddAsync(almacen);
            await _context.SaveChangesAsync();

            return Ok(almacen.AlmacenId);
        }

        // DELETE: api/Almacens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlmacen(int id)
        {
            var almacen = await _context.Almacen.FindAsync(id);
            if (almacen == null)
            {
                return NotFound();
            }

            _context.Almacen.Remove(almacen);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlmacenExists(int id)
        {
            return _context.Almacen.Any(e => e.AlmacenId == id);
        }
    }
}
