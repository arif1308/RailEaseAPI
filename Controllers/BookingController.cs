using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailEaseAPI.Data;
using RailEaseAPI.Models;

namespace RailEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // Booking karo
        [HttpPost]
        public async Task<IActionResult> BookTicket([FromBody] Booking booking)
        {
            // Train check karo
            var train = await _context.Trains
                .Include(t => t.Categories)
                .FirstOrDefaultAsync(t => t.Id == booking.TrainId);

            if (train == null)
                return NotFound("Train nahi mili!");

            // Category check karo
            var category = train.Categories
                .FirstOrDefault(c => c.Name == booking.Category);

            if (category == null)
                return NotFound("Category nahi mili!");

            // Seats check karo
            if (category.BookedSeats >= category.TotalSeats)
                return BadRequest("Seats full hain!");

            // Seat book karo
            category.BookedSeats += 1;
            booking.Status = "Confirmed";

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Booking confirmed!", bookingId = booking.Id });
        }

        // User ki saari bookings
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserBookings(int userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Train)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            return Ok(bookings);
        }

        // Booking cancel karo
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.Train)
                .ThenInclude(t => t.Categories)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound("Booking nahi mili!");

            // Seat wapas karo
            var category = booking.Train!.Categories
                .FirstOrDefault(c => c.Name == booking.Category);

            if (category != null)
                category.BookedSeats -= 1;

            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();

            return Ok("Booking cancelled!");
        }
    }
}