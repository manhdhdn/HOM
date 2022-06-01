/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;

namespace HOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly DataContext _context;

        public BillsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bills>>> GetBills()
        {
          if (_context.Bills == null)
          {
              return NotFound();
          }
            return await _context.Bills.ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bills>> GetBills(int id)
        {
          if (_context.Bills == null)
          {
              return NotFound();
          }
            var bills = await _context.Bills.FindAsync(id);

            if (bills == null)
            {
                return NotFound();
            }

            return bills;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBills(int id, Bills bills)
        {
            if (id != bills.Id)
            {
                return BadRequest();
            }

            _context.Entry(bills).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillsExists(id))
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

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bills>> PostBills(Bills bills)
        {
          if (_context.Bills == null)
          {
              return Problem("Entity set 'DataContext.Bills'  is null.");
          }
            _context.Bills.Add(bills);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBills", new { id = bills.Id }, bills);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBills(int id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bills = await _context.Bills.FindAsync(id);
            if (bills == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bills);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillsExists(int id)
        {
            return (_context.Bills?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/