using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonApi.Models;

namespace PersonApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonDetailsController : ControllerBase
    {
        private readonly PersonContext _context;

        public PersonDetailsController(PersonContext context)
        {
            _context = context;
        }

        // GET: api/PersonDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonDetails>>> GetPersonDetailss()
        {
            return await _context.PersonDetailss.ToListAsync();
        }

        // GET: api/PersonDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonDetails>> GetPersonDetails(int id)
        {
            var personDetails = await _context.PersonDetailss.FindAsync(id);

            if (personDetails == null)
            {
                return NotFound();
            }

            return personDetails;
        }

        // PUT: api/PersonDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonDetails(int id, PersonDetails personDetails)
        {
            if (id != personDetails.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(personDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonDetailsExists(id))
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

        // POST: api/PersonDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PersonDetails>> PostPersonDetails(PersonDetails personDetails)
        {
            _context.PersonDetailss.Add(personDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonDetails), new { id = personDetails.PersonId }, personDetails);
        }

        // DELETE: api/PersonDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonDetails(int id)
        {
            var personDetails = await _context.PersonDetailss.FindAsync(id);
            if (personDetails == null)
            {
                return NotFound();
            }

            _context.PersonDetailss.Remove(personDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonDetailsExists(int id)
        {
            return _context.PersonDetailss.Any(e => e.PersonId == id);
        }
    }
}
