using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Models;

namespace HOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly DataContext _context;

        public RoomsController(DataContext context)
        {
            _context = context;
        }

        /*// GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rooms>>> GetRooms()
        {
          if (_context.Rooms == null)
          {
              return NotFound();
          }
            return await _context.Rooms.ToListAsync();
        }*/

        // GET: api/Rooms/5
        [HttpGet("{hostelId}")]
        public async Task<ActionResult<IEnumerable<RoomModel>>> GetRooms(int hostelId)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }

            var rooms = (from room in _context.Rooms
                         join roomtype in _context.RoomTypes on room.TypeId equals roomtype.Id
                         join image in _context.Images on room.ImageId equals image.Id
                         where room.Status == true && room.HostelId == hostelId
                         select new RoomModel()
                         {
                             Id = room.Id,
                             Name = room.Name,
                             ImageUrl = image.Url,
                             RoomType = roomtype.Name,
                             Acreage = room.Acreage,
                             Price = room.Price
                         }).ToListAsync();

            if (rooms.Result.Count == 0)
            {
                return NotFound();
            }

            return await rooms;
        }

        /*// PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRooms(int id, Rooms rooms)
        {
            if (id != rooms.Id)
            {
                return BadRequest();
            }

            _context.Entry(rooms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomsExists(id))
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

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rooms>> PostRooms(Rooms rooms)
        {
          if (_context.Rooms == null)
          {
              return Problem("Entity set 'DataContext.Rooms'  is null.");
          }
            _context.Rooms.Add(rooms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRooms", new { id = rooms.Id }, rooms);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRooms(int id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }
            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(rooms);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

        private bool RoomsExists(int id)
        {
            return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
