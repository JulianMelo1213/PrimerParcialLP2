using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using PrimerParcialLP2.DTO.Salida;


namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidumsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper mapper;


        public SalidumsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Salidums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalidaGetDTO>>> GetSalida()
        {
            var salidaList = await _context.Salida.ToListAsync();
            var salidaDto = mapper.Map<IEnumerable<SalidaGetDTO>>(salidaList);
            return Ok(salidaDto);
        }

        // GET: api/Salidums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SalidaGetDTO>> GetSalidum(int id)
        {
            var salida = await _context.Salida.FindAsync(id);

            if (salida == null)
            {
                return NotFound();
            }
            var salidaDto = mapper.Map<SalidaGetDTO>(salida);

            return Ok(salidaDto);
        }

        // PUT: api/Salidums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalidum(int id, SalidaPutDTO salidaDto)
        {
            if (id != salidaDto.SalidaId)
            {
                return BadRequest();
            }

            var salida = mapper.Map<Salidum>(salidaDto);
            _context.Entry(salida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalidumExists(id))
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Salidum>> PostSalidum(SalidaInsertDTO salidaDto)
        {
            var salida = mapper.Map<Salidum>(salidaDto);
            await _context.Salida.AddAsync(salida);
            await _context.SaveChangesAsync();

            return Ok(salida.SalidaId);
        }

        // DELETE: api/Salidums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalidum(int id)
        {
            var salidum = await _context.Salida.FindAsync(id);
            if (salidum == null)
            {
                return NotFound();
            }

            _context.Salida.Remove(salidum);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalidumExists(int id)
        {
            return _context.Salida.Any(e => e.SalidaId == id);
        }
    }
}
