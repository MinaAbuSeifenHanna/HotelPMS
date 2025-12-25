namespace PMS.Application.DTOs
{
    public class CreateRoomDto
    {
        public string RoomNumber { get; set; }

       
        public string RoomType { get; set; }
        public decimal PricePerNight { get; set; }
    }
}
