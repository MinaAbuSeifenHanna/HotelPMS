using System.ComponentModel.DataAnnotations;

namespace PMS.Features.Reservations.Application.DTOS
{
    public class CheckInDto
    {
     
        public string   RoomNumber { get; set; }        
        public string GuestName { get; set; }  
        public string GuestNationalId { get; set; }

        public string GuestPhone { get; set; }
        public int NumberOfNights { get; set; } 
    }
}
