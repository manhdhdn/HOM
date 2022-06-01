using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Models;
using Microsoft.AspNetCore.Authorization;

namespace HOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelsController : ControllerBase
    {
        private readonly DataContext _context;

        public HostelsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Hostels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HostelModel>>> GetHostels()
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }

            var hostels = (from hostel in _context.Hostels
                           join image in _context.Images on hostel.ImageId equals image.Id
                           join account in _context.Accounts on hostel.AccountId equals account.Id
                           where hostel.Status == true
                           select new HostelModel()
                           {
                               Id = hostel.Id,
                               Name = hostel.Name,
                               ImageUrl = image.Url,
                               Address = hostel.Address,
                               LandlordsName = account.Name
                           }).ToListAsync();

            return await hostels;
        }

        // GET: api/Hostels/5
        [Authorize(Roles = "Landlords, Admin")]
        [HttpGet("{accountId}")]
        public async Task<ActionResult<IEnumerable<HostelModel>>> GetHostels(int accountId)
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }

            var hostels = (from hostel in _context.Hostels
                           join image in _context.Images on hostel.ImageId equals image.Id
                           join account in _context.Accounts on hostel.AccountId equals account.Id
                           where hostel.Status == true && account.Id == accountId
                           select new HostelModel()
                           {
                               Id = hostel.Id,
                               Name = hostel.Name,
                               ImageUrl = image.Url,
                               Address = hostel.Address,
                               LandlordsName = account.Name
                           }).ToListAsync();

            if (hostels.Result.Count == 0)
            {
                return NotFound();
            }

            return await hostels;
        }

        /*// PUT: api/Hostels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHostels(int id, Hostels hostels)
        {
            if (id != hostels.Id)
            {
                return BadRequest();
            }

            _context.Entry(hostels).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostelsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Hostels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hostels>> PostHostels(Hostels hostels)
        {
            if (_context.Hostels == null)
            {
                return Problem("Entity set 'DataContext.Hostels'  is null.");
            }

            _context.Hostels.Add(hostels);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHostels", new { id = hostels.Id }, hostels);
        }

        /*// DELETE: api/Hostels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHostels(int id)
        {
            if (_context.Hostels == null)
            {
                return NotFound();
            }
            var hostels = await _context.Hostels.FindAsync(id);
            if (hostels == null)
            {
                return NotFound();
            }

            _context.Hostels.Remove(hostels);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool HostelsExists(int id)
        {
            return (_context.Hostels?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
