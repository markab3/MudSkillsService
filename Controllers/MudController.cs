using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudSkillsService.Helpers;
using MudSkillsService.Models;

namespace MudSkillsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MudController : ControllerBase
    {
        private readonly MudSkillsContext _context;

        public MudController(MudSkillsContext context)
        {
            _context = context;
        }

        // GET: api/Mud
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mud>>> GetMud()
        {
            return await _context.Mud.ToListAsync();
        }

        // GET: api/Mud/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mud>> GetMud(int id)
        {
            var mud = await _context.Mud.FindAsync(id);

            if (mud == null)
            {
                return NotFound();
            }

            return mud;
        }

        // PUT: api/Mud/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMud(int id, Mud mud)
        {
            if (id != mud.MudId)
            {
                return BadRequest();
            }

            _context.Entry(mud).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MudExists(id))
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

        // POST: api/Mud
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mud>> PostMud(Mud mud)
        {
            _context.Mud.Add(mud);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMud", new { id = mud.MudId }, mud);
        }

        // DELETE: api/Mud/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMud(int id)
        {
            var mud = await _context.Mud.FindAsync(id);
            if (mud == null)
            {
                return NotFound();
            }

            _context.Mud.Remove(mud);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MudExists(int id)
        {
            return _context.Mud.Any(e => e.MudId == id);
        }
    }
}
