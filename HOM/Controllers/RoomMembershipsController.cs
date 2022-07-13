using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using HOM.Repository;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class RoomMembershipsController : ControllerBase
    {
        private readonly HOMContext _context;

        public RoomMembershipsController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/RoomMemberships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomMembership>>> GetRoomMemberships([Required] string roomId)
        {
            if (_context.RoomMemberships == null)
            {
                return NotFound();
            }

            var source = _context.RoomMemberships.Where(r => r.RoomId == roomId);

            return await source.ToListAsync();
        }

        // GET: api/RoomMemberships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomMembership>> GetRoomMembership(string id)
        {
            if (_context.RoomMemberships == null)
            {
                return NotFound();
            }
            var roomMembership = await _context.RoomMemberships.FindAsync(id);

            if (roomMembership == null)
            {
                return NotFound();
            }

            return roomMembership;
        }

        // PUT: api/RoomMemberships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomMembership(string id, RoomMembership roomMembership)
        {
            if (id != roomMembership.Id)
            {
                return BadRequest();
            }

            if (RoomMembershipExists(roomMembership, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), roomMembership.GetType(), ModelState));
            }

            _context.Entry(roomMembership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RoomMembershipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, roomMembership.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/RoomMemberships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RoomMembership>> PostRoomMembership(RoomMembership roomMembership)
        {
            if (_context.RoomMemberships == null)
            {
                return Problem("Entity set 'HOMContext.RoomMemberships'  is null.");
            }

            if (RoomMembershipExists(roomMembership, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), roomMembership.GetType(), ModelState));
            }

            roomMembership.Id = Guid.NewGuid().ToString();
            _context.RoomMemberships.Add(roomMembership);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (RoomMembershipExists(roomMembership.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, roomMembership.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetRoomMembership", new { id = roomMembership.Id }, roomMembership);
        }

        // DELETE: api/RoomMemberships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoomMembership(string id)
        {
            if (_context.RoomMemberships == null)
            {
                return NotFound();
            }
            var roomMembership = await _context.RoomMemberships.FindAsync(id);
            if (roomMembership == null)
            {
                return NotFound();
            }

            _context.RoomMemberships.Remove(roomMembership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomMembershipExists(string id) => (_context.RoomMemberships?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool RoomMembershipExists(RoomMembership membership, bool method)
        {
            bool result = true;

            var id = _context.RoomMemberships.Where(r => r.AccountId == membership.AccountId && r.RoomId == membership.RoomId)
                .Select(r => r.Id).FirstOrDefault();

            if (id == null || (id == membership.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
