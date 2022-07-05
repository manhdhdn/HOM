using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using HOM.Repository;
using HOM.Models;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HostelsController : ControllerBase
    {
        private readonly HOMContext _context;

        public HostelsController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Hostels
        [HttpGet]
        public async Task<ActionResult<PagedModel<Hostel>>> GetHostels(int pageIndex, int pageSize, string? accountId)
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }

            var source = _context.Hostels.Include(h => h.Images).Include(h => h.Services).Include(h => h.RoomTypes).Include(h => h.Account)
                .OrderBy(h => h.Name)
                .AsQueryable();

            if (accountId != null)
            {
                source = source.Where(s => s.AccountId == accountId);
            }

            return await PaginatedList<Hostel>.CreateAsync(source, pageIndex, pageSize);
        }

        // GET: api/Hostels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hostel>> GetHostel(string id)
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }
            var hostel = await _context.Hostels.FindAsync(id);

            if (hostel == null)
            {
                return NotFound();
            }

            return hostel;
        }

        // PUT: api/Hostels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHostel(string id, HostelModel hostel)
        {
            if (id != hostel.Id)
            {
                return BadRequest();
            }

            if (HostelExists(hostel, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), hostel.GetType(), ModelState));
            }

            _context.Entry(new Hostel(hostel, false)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!HostelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, hostel.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/Hostels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HostelModel>> PostHostel(HostelModel hostel)
        {
            if (_context.Hostels == null)
            {
                return Problem("Entity set 'HOMContext.Hostels'  is null.");
            }

            if (HostelExists(hostel, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), hostel.GetType(), ModelState));
            }

            _context.Hostels.Add(new Hostel(hostel, true));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (HostelExists(hostel.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, hostel.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetHostel", new { id = hostel.Id }, hostel);
        }

        // DELETE: api/Hostels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHostel(string id)
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }
            var hostel = await _context.Hostels.FindAsync(id);
            if (hostel == null)
            {
                return NotFound();
            }

            _context.Hostels.Remove(hostel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HostelExists(string id) => (_context.Hostels?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool HostelExists(HostelModel hostel, bool method)
        {
            bool result = true;

            var id = _context.Hostels.Where(h => h.Name == hostel.Name && h.Address == hostel.Address && h.AccountId == hostel.AccountId).Select(h => h.Id).FirstOrDefault();

            if (id == null || (id == hostel.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
