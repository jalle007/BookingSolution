using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BookingApi.Models;

namespace BookingApi.Controllers
{
  [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CapacityController : ControllerBase
    {
        private readonly BookingContext _context;

        public CapacityController(BookingContext context)
        {
            _context = context;

    }

        [HttpGet]
        public ActionResult<List<CapacityProvider>> GetAll()
        {
            return _context.CapacityProviders.ToList();
        }

        [HttpGet("{id}", Name = "GetCapacityProvider")]
        public ActionResult<CapacityProvider> GetById(int id)
        {
            var item = _context.CapacityProviders.Find(id);

            if (item == null)
                return NotFound();

            return item;
        }
        
        /// <summary>
        /// Creates a CapacityProvider.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>A newly created CapacityProvider</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>            
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<CapacityProvider> Create(CapacityProvider item)
        {
            _context.CapacityProviders.Add(item);
            _context.SaveChanges();
      
            return CreatedAtRoute("GetCapacityProvider", new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CapacityProvider item)
        {
            if (item == null || item.id != id)
            {
                return BadRequest();
            }

            var CapacityProvider = _context.CapacityProviders.Find(id);

            if (CapacityProvider == null)
            {
                return NotFound();
            }
      CapacityProvider.name = item.name;
      CapacityProvider.city = item.city;

      _context.CapacityProviders.Update(CapacityProvider);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific CapacityProvider.
        /// </summary>
        /// <param name="id"></param>        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var CapacityProvider = _context.CapacityProviders.Find(id);

            if (CapacityProvider == null)
                return NotFound();

            _context.CapacityProviders.Remove(CapacityProvider);
            _context.SaveChanges();

            return NoContent();
        }
    }
}