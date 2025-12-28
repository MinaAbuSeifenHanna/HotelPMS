namespace PMS.Features.Rooms.Application.DTOS
{
    public class RoomStatisticsDto
    {
        public int Available { get; set; }
        public int Occupied { get; set; }
        public int Reserved { get; set; }
        public int Cleaning { get; set; }
        public int Maintenance { get; set; }
    }
}
