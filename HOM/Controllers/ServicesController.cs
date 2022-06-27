using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using System.ComponentModel.DataAnnotations;
using HOM.Repository;

namespace HOM.Controllers
{
    [Route("[controller]")]
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

            if (ServiceExists(service, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), service.GetType(), ModelState));
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ServiceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, service.GetType(), ModelState));
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

            if (ServiceExists(service, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), service.GetType(), ModelState));
            }

            service.Id = Guid.NewGuid().ToString();
            _context.Services.Add(service);


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ServiceExists(service.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, service.GetType(), ModelState));
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

        private bool ServiceExists(string id) => (_context.Services?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool ServiceExists(Service service, bool method)
        {
            bool result = true;

            var id = _context.Services.Where(s => s.Name == service.Name && s.HostelId == service.HostelId).Select(s => s.Id).FirstOrDefault();

            if (id == null || (id == service.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
