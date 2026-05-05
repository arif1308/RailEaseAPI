namespace RailEaseAPI.Models
{
    public class TrainCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; } = 0;
        public int TrainId { get; set; }
        public Train? Train { get; set; }
    }
}