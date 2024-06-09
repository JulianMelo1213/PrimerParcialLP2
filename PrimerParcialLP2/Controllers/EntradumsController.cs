using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using PrimerParcialLP2.DTO.Entrada;



namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntradumsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper mapper;

        public EntradumsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Entradums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EntradaGetDTO>>> GetEntrada()
        {
            var entradaList = await _context.Entrada.ToListAsync();
            var entradaDto = mapper.Map<IEnumerable<EntradaGetDTO>>(entradaList);
            return Ok(entradaDto);
        }

        // GET: api/Entradums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EntradaGetDTO>> GetEntradum(int id)
        {
            var entrada = await _context.Entrada.FindAsync(id);

            if (entrada == null)
            {
                return NotFound();
            }
            var entradaDto = mapper.Map<EntradaGetDTO>(entrada);

            return Ok(entradaDto);
        }

        // PUT: api/Entradums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntradum(int id, EntradaPutDTO entradaDTO)
        {
            if (id != entradaDTO.EntradaId)
            {
                return BadRequest();
            }

            var entrada = mapper.Map<Entradum>(entradaDTO);
            _context.Entry(entrada).State = EntityState.Modified;

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Entradum>> PostEntradum(EntradaInsertDTO entradaDto)
        {
            var entrada = mapper.Map<Entradum>(entradaDto);
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
