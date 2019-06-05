using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Api;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderboardsController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public LeaderboardsController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Leaderboards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Leaderboard>>> GetLeaderboard()
        {
            return await _context.Leaderboard.ToListAsync();
        }

        // GET: api/Leaderboards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Leaderboard>> GetLeaderboard(Guid id)
        {
            var leaderboard = await _context.Leaderboard.FindAsync(id);

            if (leaderboard == null)
            {
                return NotFound();
            }

            return leaderboard;
        }

        // PUT: api/Leaderboards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLeaderboard(Guid id, Leaderboard leaderboard)
        {
            if (id != leaderboard.Id)
            {
                return BadRequest();
            }

            _context.Entry(leaderboard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaderboardExists(id))
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

        // POST: api/Leaderboards
        [HttpPost]
        public async Task<ActionResult<Leaderboard>> PostLeaderboard(Leaderboard leaderboard)
        {
            _context.Leaderboard.Add(leaderboard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLeaderboard", new { id = leaderboard.Id }, leaderboard);
        }

        // DELETE: api/Leaderboards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Leaderboard>> DeleteLeaderboard(Guid id)
        {
            var leaderboard = await _context.Leaderboard.FindAsync(id);
            if (leaderboard == null)
            {
                return NotFound();
            }

            _context.Leaderboard.Remove(leaderboard);
            await _context.SaveChangesAsync();

            return leaderboard;
        }

        private bool LeaderboardExists(Guid id)
        {
            return _context.Leaderboard.Any(e => e.Id == id);
        }
    }
}
