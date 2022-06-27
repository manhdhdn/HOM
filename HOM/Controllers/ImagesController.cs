using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HOM.Data;
using HOM.Data.Context;
using HOM.Repository;
using System.ComponentModel.DataAnnotations;
using HOM.Models;

namespace HOM.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly HOMContext _context;

        public ImagesController(HOMContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<PagedModel<Image>>> GetImages(int pageIndex, int pageSize, [Required] string hostelId, string? roomId)
        {
            if (_context.Images == null)
            {
                return NotFound();
            }

            var source = _context.Images.Where(i => i.HostelId == hostelId && i.RoomId == null);

            if (roomId != null)
            {
                source = source.Where(i => i.RoomId == roomId);
            }

            return await PaginatedList<Image>.CreateAsync(source, pageIndex, pageSize);
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImage(string id)
        {
            if (_context.Images == null)
            {
                return NotFound();
            }
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return image;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImage(string id, Image image)
        {
            if (id != image.Id)
            {
                return BadRequest();
            }

            if (ImageExists(image, false))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist, can not save changes."), image.GetType(), ModelState));
            }

            _context.Entry(image).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, image.GetType(), ModelState));
                }
            }

            return NoContent();
        }

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Image>> PostImage(Image image)
        {
            if (_context.Images == null)
            {
                return Problem("Entity set 'HOMContext.Images'  is null.");
            }

            if (ImageExists(image, true))
            {
                return ValidationProblem(ExceptionHandle.Handle(new Exception("Already exist."), image.GetType(), ModelState));
            }

            image.Id = Guid.NewGuid().ToString();
            _context.Images.Add(image);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ImageExists(image.Id))
                {
                    return Conflict();
                }
                else
                {
                    return ValidationProblem(ExceptionHandle.Handle(ex, image.GetType(), ModelState));
                }
            }

            return CreatedAtAction("GetImage", new { id = image.Id }, image);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(string id)
        {
            if (_context.Images == null)
            {
                return NotFound();
            }
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }

            _context.Images.Remove(image);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageExists(string id) => (_context.Images?.Any(e => e.Id == id)).GetValueOrDefault();

        private bool ImageExists(Image image, bool method)
        {
            bool result = true;

            var id = _context.Images.Where(i => i.Url == image.Url && i.HostelId == image.HostelId && i.RoomId == image.RoomId).Select(i => i.Id).FirstOrDefault();

            if (id == null || (id == image.Id && !method))
            {
                result = false;
            }

            return result;
        }
    }
}
