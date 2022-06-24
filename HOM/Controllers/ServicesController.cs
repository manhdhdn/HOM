using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using System.ComponentModel.DataAnnotations;

namespace HOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly HOMContext _context;

        public ServicesController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices([Required] string hostelId)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }

            var source = _context.Services.Where(s => s.HostelId == hostelId);

            return await source.ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(string id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(string id, Service service)
        {
            if (id != service.Id)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'HOMContext.Services'  is null.");
            }
            _context.Services.Add(service);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceExists(service.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetService", new { id = service.Id }, service);
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(string id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(string id)
        {
            return (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
