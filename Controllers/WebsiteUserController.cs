using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudSkillsService.Helpers;
using MudSkillsService.Models;

namespace MudSkillsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebsiteUserController : ControllerBase
    {
        private readonly MudSkillsContext _context;

        public WebsiteUserController(MudSkillsContext context)
        {
            _context = context;
        }

        // GET: api/WebsiteUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WebsiteUser>>> GetWebsiteUsers([FromQuery] bool includeDeleted = false)
        {
            if (includeDeleted)
            {
                return await _context.WebsiteUsers.ToListAsync();
            }
            return await _context.WebsiteUsers.Where(w => w.DateDeleted == null).ToListAsync();
        }

        // GET: api/WebsiteUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WebsiteUser>> GetWebsiteUser(int id, [FromQuery] bool includeDeleted = false)
        {
            var websiteUser = includeDeleted
                ? await _context.WebsiteUsers.FindAsync(id)
                : await _context.WebsiteUsers.Where(w => w.WebsiteUserId == id && w.DateDeleted == null).FirstOrDefaultAsync();

            if (websiteUser == null)
            {
                return NotFound();
            }

            return websiteUser;
        }

        // PUT: api/WebsiteUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWebsiteUser(int id, WebsiteUser websiteUser)
        {
            if (id != websiteUser.WebsiteUserId)
            {
                return BadRequest();
            }

            _context.Entry(websiteUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsiteUserExists(id))
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

        // POST: api/WebsiteUser
        [HttpPost]
        public async Task<ActionResult<WebsiteUser>> PostWebsiteUser(WebsiteUser websiteUser)
        {
            _context.WebsiteUsers.Add(websiteUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebsiteUser", new { id = websiteUser.WebsiteUserId }, websiteUser);
        }

        // DELETE: api/WebsiteUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWebsiteUser(int id)
        {
            var websiteUser = await _context.WebsiteUsers.FindAsync(id);
            if (websiteUser == null)
            {
                return NotFound();
            }

            websiteUser.DateDeleted = DateTime.UtcNow;
            _context.Entry(websiteUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WebsiteUserExists(int id)
        {
            return _context.WebsiteUsers.Any(e => e.WebsiteUserId == id);
        }
    }
}
