namespace PMS.Features.Rooms.Application.DTOS
{
    public class RoomResultDto
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; } 
        public int Floor { get; set; }
        public string Status { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
