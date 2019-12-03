using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BookingApi.Models;
using BookingAPI.Providers;

namespace BookingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingContext _context;

        public BookingController(BookingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Booking>> GetAll()
        {
            
            var bookings = _context.Bookings.ToList();

            return bookings;
        }

        [HttpGet("{id}", Name = "GetBooking")]
        public ActionResult<Booking> GetById(int id)
        {
            var item = _context.Bookings.Find(id);
            if (item == null)
                return NotFound();


            return item;
        }

        /// <summary>
        /// Seed data from Excel file first.
        /// </summary>
        [HttpGet("GetSeed", Name = "GetSeed")]
        public ActionResult<List<Booking>> GetSeed()
        {
            var xls = new ExcelProvider(_context);
            var res = xls.ParseData();

            return null;
        }

        /// <summary>
        /// Creates a Booking.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created Booking</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Booking> Create(Booking item)
        {

            _context.Bookings.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetBooking", new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Booking item)
        {
            if (item == null || item.id != id)
                return BadRequest();

            var Booking = _context.Bookings.Find(id);

            if (Booking == null)
                return NotFound();

            _context.Bookings.Update(Booking);
            _context.SaveChanges();

            return NoContent();
        }

      
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);

            if (booking == null)
                return NotFound();


            _context.Bookings.Remove(booking);
            _context.SaveChanges();

            // Cascade delete not possible. Wrong relationship
            //var capacity = _context.CapacityProviders.Find(booking.CapacityProviderId);
            //if (capacity != null)
            //{
            //    _context.CapacityProviders.Remove(capacity);
            //    _context.SaveChanges();
            //}

            return NoContent();
        }
    }
}