using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using System.ComponentModel.DataAnnotations;
using HOM.Models;
using HOM.Repository;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly HOMContext _context;

        public BookingsController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<PagedModel<Booking>>> GetBookings(int pageIndex, int pageSize, [Required] string hostelId)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }

            var source = _context.Bookings.Join(_context.Rooms
                .Where(r => r.HostelId == hostelId),
                booking => booking.RoomId,
                room => room.Id,
                (booking, room) => new Booking());

            return await PaginatedList<Booking>.CreateAsync(source, pageIndex, pageSize);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(string id)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);

            if (booking == null)
            {
                return NotFound();
            }

            return booking;
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(string id, Booking booking)
        {
            if (id != booking.Id)
            {
                return BadRequest();
            }

            if (BookingExists(booking, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), booking.GetType(), ModelState));
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, booking.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            if (_context.Bookings == null)
            {
                return Problem("Entity set 'HOMContext.Bookings'  is null.");
            }

            if (BookingExists(booking, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), booking.GetType(), ModelState));
            }

            booking.Id = Guid.NewGuid().ToString();
            _context.Bookings.Add(booking);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (BookingExists(booking.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, booking.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetBooking", new { id = booking.Id }, booking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(string id)
        {
            if (_context.Bookings == null)
            {
                return NotFound();
            }
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(string id) => (_context.Bookings?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool BookingExists(Booking booking, bool method)
        {
            bool result = true;

            var id = _context.Bookings.Where(b => b.AccountId == booking.AccountId && b.RoomId == booking.RoomId)
                .Select(b => b.Id).FirstOrDefault();

            if (id == null || (id == booking.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
