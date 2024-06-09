using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrimerParcialLP2.Models;
using AutoMapper;
using PrimerParcialLP2.DTO.Categoria;


namespace PrimerParcialLP2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriumsController : ControllerBase
    {
        private readonly GestionInventariosContext _context;
        private readonly IMapper mapper;

        public CategoriumsController(GestionInventariosContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Categoriums
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaGetDTO>>> GetCategoria()
        {
            var categoriaList = await _context.Categoria.ToListAsync();
            var categoriaDto = mapper.Map<IEnumerable<CategoriaGetDTO>>(categoriaList);
            return Ok(categoriaDto);
        }

        // GET: api/Categoriums/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriaGetDTO>> GetCategorium(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }
            var categoriaDto = mapper.Map<CategoriaGetDTO>(categoria);


            return Ok(categoriaDto);
        }

        // PUT: api/Categoriums/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategorium(int id, CategoriaPutDTO categoriaDto)
        {
            if (id != categoriaDto.CategoriaId)
            {
                return BadRequest();
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            mapper.Map(categoriaDto, categoria);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriumExists(id))
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
        // POST: api/Categoriums
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Categorium>> PostCategorium(CategoriaInsertDTO categoriaDto)
        {
            var categoria = mapper.Map<Categorium>(categoriaDto);
            await _context.Categoria.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria.CategoriaId);
        }

        // DELETE: api/Categoriums/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategorium(int id)
        {
            var categorium = await _context.Categoria.FindAsync(id);
            if (categorium == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categorium);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriumExists(int id)
        {
            return _context.Categoria.Any(e => e.CategoriaId == id);
        }
    }
}
