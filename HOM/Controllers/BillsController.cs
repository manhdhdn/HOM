using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using HOM.Models;
using HOM.Repository;
using System.ComponentModel.DataAnnotations;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly HOMContext _context;

        public BillsController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<PagedModel<Bill>>> GetBills(int pageIndex, int pageSize, [Required] string hotelId, string? roomId)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }

            var source = _context.Bills.GroupJoin(_context.Rooms.Where(r => r.HostelId == hotelId),
                bill => bill.RoomId,
                room => room.Id,
                (bill, room) => new Bill());

            if (roomId != null)
            {
                source = source.Where(s => s.RoomId == roomId);
            }

            return await PaginatedList<Bill>.CreateAsync(source, pageIndex, pageSize);
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bill>> GetBill(string id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.FindAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(string id, Bill bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            if (BillExists(bill, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), bill.GetType(), ModelState));
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, bill.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            if (_context.Bills == null)
            {
                return Problem("Entity set 'HOMContext.Bills'  is null.");
            }

            if (BillExists(bill, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), bill.GetType(), ModelState));
            }

            bill.Id = Guid.NewGuid().ToString();
            _context.Bills.Add(bill);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (BillExists(bill.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, bill.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBill(string id)
        {
            if (_context.Bills == null)
            {
                return NotFound();
            }
            var bill = await _context.Bills.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BillExists(string id) => (_context.Bills?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool BillExists(Bill bill, bool method)
        {
            bool result = true;

            var id = _context.Bills.Where(b => b.ServiceId == bill.ServiceId && b.RoomId == bill.RoomId && b.Date < (DateTime.Now - new System.TimeSpan(28, 0, 0, 0)))
            .Select(b => b.Id).FirstOrDefault();

            if (id == null || (id == bill.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
