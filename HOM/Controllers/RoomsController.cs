﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class RoomsController : ControllerBase
    {
        private readonly HOMContext _context;

        public RoomsController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<PagedModel<Room>>> GetRooms(int pageIndex, int pageSize, [Required] string hostelId)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }

            var source = _context.Rooms.Where(r => r.HostelId == hostelId).AsQueryable();

            return await PaginatedList<Room>.CreateAsync(source, pageIndex, pageSize);
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(string id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);

            if (room == null)
            {
                return NotFound();
            }

            return room;
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(string id, RoomModel room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            if (RoomExists(room))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), room.GetType(), ModelState));
            }

            _context.Entry(new Room(room, false)).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, room.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Room>> PostRoom(RoomModel room)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'HOMContext.Rooms'  is null.");
            }

            if (RoomExists(room))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), room.GetType(), ModelState));
            }

            _context.Rooms.Add(new Room(room, true));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (RoomExists(room.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, room.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetRoom", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(string id)
        {
            if (_context.Rooms == null)
            {
                return NotFound();
            }
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoomExists(string id) => (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool RoomExists(RoomModel room)
        {
            bool result = true;

            var id = _context.Rooms.Where(r => r.Name == room.Name && r.HostelId == room.HostelId).Select(r => r.Id).FirstOrDefault();

            if (id == null || id == room.Id)
            {
                result = false;
            }

            return result;
        }
    }
}
