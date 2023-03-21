using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hotel_Web_API.Models;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Hosting;
using System.Security.Policy;
using Microsoft.Extensions.Hosting;

namespace Hotel_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly HotelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelsController(HotelContext context)
        {
            _context = context;
        }
        

        // GET: api/Hotels
        [HttpGet]

        public async Task<ActionResult<IEnumerable<Hotel>>> GetHotel()
        {
           

            if (_context.Hotel == null)
          {
              return NotFound();
          }

          
            return await _context.Hotel.ToListAsync();
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
          if (_context.Hotel == null)
          {
              return NotFound();
          }
            var hotel = await _context.Hotel.FindAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, Hotel hotel)
        {
            if (id != hotel.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(id))
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

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Hotel>>> createHotel( Hotel hotel)
        {

           
            string path = Path.Combine("C:\\Users\\Win10\\Desktop\\aziz\\hotel\\src\\assets\\img\\", hotel.file.FileName);
            //var fileProvider = new PhysicalFileProvider(Path.Combine(path1: builder.Environment.ContentRootPath, "Images"));
           

            using (Stream stream = new FileStream(path,FileMode.Create))
            {
                hotel.file.CopyTo(stream);
                }
            hotel.Image = hotel.file.FileName;
            _context.Hotel.Add(hotel);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHotel", new { id = hotel.HotelId }, hotel);
        }


        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            if (_context.Hotel == null)
            {
                return NotFound();
            }
            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelExists(int id)
        {
            return (_context.Hotel?.Any(e => e.HotelId == id)).GetValueOrDefault();
        }



       
        
            private readonly IWebHostEnvironment _hostingEnvironment;

         
        




    }
}
