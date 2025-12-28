using PMS.Features.Reservations.Domain.Entities;

namespace PMS.Features.Guests.Domain.Entities
{
    public class Companion
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public int Age { get; set; }

        //[Required]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
