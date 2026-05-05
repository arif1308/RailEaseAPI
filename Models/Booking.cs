namespace RailEaseAPI.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string PassengerName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Price { get; set; }
        public string TravelDate { get; set; } = string.Empty;
        public string Status { get; set; } = "Confirmed";
        public int UserId { get; set; }
        public User? User { get; set; }
        public int TrainId { get; set; }
        public Train? Train { get; set; }
    }
}