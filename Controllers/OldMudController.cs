using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudSkillsService.Helpers;
using MudSkillsService.Models;

namespace MudSkillsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OldMudController : Controller
    {
        public MudSkillsContext _context;

        public OldMudController(MudSkillsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mud>>> GetAll()
        {
            var results = await _context.Mud.ToListAsync();
            return new OkObjectResult(results);
        }
    }
}
