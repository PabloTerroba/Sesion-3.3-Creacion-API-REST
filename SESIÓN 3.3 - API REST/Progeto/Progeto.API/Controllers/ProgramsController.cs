using Microsoft.AspNetCore.Mvc;
using Progeto.API.Models;
using Progeto.API.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Progeto.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProgramsController : ControllerBase
    {
        private readonly ProgramDBContext _context;

        public ProgramsController(ProgramDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LuaProgram>>> GetAll()
        {
            return await _context.Programs.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LuaProgram>> GetById(string id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null) return NotFound();
            return program;
        }

        [HttpPost]
        public async Task<ActionResult<LuaProgram>> Create(LuaProgram program)
        {
            _context.Programs.Add(program);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = program.Id }, program);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, LuaProgram program)
        {
            if (id != program.Id) return BadRequest();
            _context.Entry(program).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var program = await _context.Programs.FindAsync(id);
            if (program == null) return NotFound();
            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
