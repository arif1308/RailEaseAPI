using Microsoft.AspNetCore.Mvc;
using RailEaseAPI.Data;
using RailEaseAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RailEaseAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainController(AppDbContext context)
        {
            _context = context;
        }

        // Sabhi trains get karo
        [HttpGet]
        public IActionResult GetAllTrains()
        {
            var trains = _context.Trains
                .Include(t => t.Categories)
                .ToList();
            return Ok(trains);
        }

        // From aur To se trains search karo
        [HttpGet("search")]
        public IActionResult SearchTrains(string from, string to)
        {
            var trains = _context.Trains
                .Include(t => t.Categories)
                .Where(t => t.From.ToLower() == from.ToLower() && t.To.ToLower() == to.ToLower())
                .ToList();

            if (trains.Count == 0)
                return NotFound("Koi train nahi mili!");

            return Ok(trains);
        }

        // Train add karo (Admin ke liye)
        [HttpPost]
        public async Task<IActionResult> AddTrain([FromBody] Train train)
        {
            _context.Trains.Add(train);
            await _context.SaveChangesAsync();
            return Ok("Train added successfully!");
        }
    }
}