using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    // This controller responds to web API requests
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        private readonly IMyDependency _myDependency;

        private readonly ILogger<UserController> _logger;

        public UserController(UserContext context, IMyDependency myDependency, ILogger<UserController> logger)
        {
            _context = context;
            _myDependency = myDependency;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetUsers()
        {
            try
            {
                _logger.LogInformation("Request for post has been made");
                _myDependency.LogMessage("Request for post has been made");
                var user = await _context.Users.ToListAsync();
                return Ok(new {message = "Success", data = user});
            }
            catch (Exception error)
            {
                _logger.LogError(error.Message);
                return StatusCode(500, new { message = "An Expected Error Occured", data = error.Message });
            }
        }

        // GET: users/5
        [HttpGet("{id}")] 
        public async Task<ActionResult<User>> GetUser(long id) 
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) { 
                return BadRequest(new {message = "User Not Found", data = (object)null });
            }
            return Ok(new {message = "Success", data = user });
        }

        // PUT: users/id
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(long id, User user)
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

        // POST: users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            try {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            // return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            return Ok(new { message = "Success", data = user});
            }
            catch(Exception error) {
                _logger.LogError(error.Message);
                return StatusCode(500, new { message = "An Expected Error Occured", data = error.Message });
            } 
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound( new {message = "User Not Found", data = (object)null});
            }

           _context.Users.Remove(user);
           await _context.SaveChangesAsync();

           return Ok(new { message = "Success", data = user });
            // return NoContent();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}