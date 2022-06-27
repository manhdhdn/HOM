using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using HOM.Repository;
using System.ComponentModel.DataAnnotations;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomTypesController : ControllerBase
    {
        private readonly HOMContext _context;

        public RoomTypesController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/RoomTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes([Required] string hostelId)
        {
            if (_context.RoomTypes == null)
            {
                return NotFound();
            }

            var source = _context.RoomTypes.Where(rt => rt.HostelId == hostelId);

            return await source.ToListAsync();
        }

        // GET: api/RoomTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomType>> GetRoomType(string id)
        {
            if (_context.RoomTypes == null)
            {
                return NotFound();
            }
            var roomType = await _context.RoomTypes.FindAsync(id);

            if (roomType == null)
            {
                return NotFound();
            }

            return roomType;
        }

        // PUT: api/RoomTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(string id, RoomType roomType)
        {
            if (id != roomType.Id)
            {
                return BadRequest();
            }

            if (RoomTypeExists(roomType, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), roomType.GetType(), ModelState));
            }

            _context.Entry(roomType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RoomTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, roomType.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/RoomTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomType>> PostRoomType(RoomType roomType)
        {
            if (_context.RoomTypes == null)
            {
                return Problem("Entity set 'HOMContext.RoomTypes'  is null.");
            }

            if (RoomTypeExists(roomType, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), roomType.GetType(), ModelState));
            }

            roomType.Id = Guid.NewGuid().ToString();
            _context.RoomTypes.Add(roomType);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (RoomTypeExists(roomType.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, roomType.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetRoomType", new { id = roomType.Id }, roomType);
        }

        // DELETE: api/RoomTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomType(string id)
        {
            if (_context.RoomTypes == null)
            {
                return NotFound();
            }
            var roomType = await _context.RoomTypes.FindAsync(id);
            if (roomType == null)
            {
                return NotFound();
            }

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomTypeExists(string id) => (_context.RoomTypes?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool RoomTypeExists(RoomType roomType, bool method)
        {
            bool result = true;

            var id = _context.RoomTypes.Where(r => r.Name == roomType.Name && r.HostelId == roomType.HostelId).Select(r => r.Id).FirstOrDefault();

            if (id == null || (id == roomType.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
