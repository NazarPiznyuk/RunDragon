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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public UsersController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("{Login}/{Password}")]
        public ActionResult<User> GetUser(string Login, string Password)
        {
            var user = _context.Users.Where(x => x.Login == Login);

            if (user == null || !user.Any())
            {
                return NotFound("Username not found");
            }
            if(user.First().Password != Password)
            {
                return NotFound("Invalid password");
            }

            return user.First();
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody]User user)
        {
            if(_context.Users.Any(x=>x.Login == user.Login ))
            {
                return Conflict("Login already exist");
            }

            if (_context.Users.Any(x=>x.Email == user.Email ))
            {
                return Conflict("Email already exist");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        //[HttpPost]
        //public async Task<ActionResult<User>> RegisterUser(string user)
        //{

        //    string fortest = user;

        //    return Ok();

        //    //if(_context.Users.Any(x=>x.Login == user.Login ))
        //    //{
        //    //    return Conflict("Login already exist");
        //    //}

        //    //if (_context.Users.Any(x=>x.Email == user.Email ))
        //    //{
        //    //    return Conflict("Email already exist");
        //    //}

        //    //_context.Users.Add(user);
        //    //await _context.SaveChangesAsync();

        //    //return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
