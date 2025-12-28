namespace PMS.Features.RoomServiceRequests.Application.DTOS
{
    public class RoomHousekeepingDto
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; } // Single, Double..
        public int Floor { get; set; }
        public decimal PricePerNight { get; set; }

        public string Status { get; set; } // Clean, Needs Cleaning, etc.

    
        public bool IsOccupied { get; set; }


        public int PendingServiceRequestsCount { get; set; }
    }
}
