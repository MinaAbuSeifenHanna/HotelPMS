

using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.entity
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(20)]
        public string NationalId { get; set; } // رقم البطاقة أو الباسبورت


        public string Phone { get; set; }

        //  (One-to-Many)
        // one guest can have many reservations
        public ICollection<Reservation> Reservations { get; set; }

    }
}
