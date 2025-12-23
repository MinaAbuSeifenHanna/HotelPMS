

using PMS.Domain.enums.GuestEnums;
using System.ComponentModel.DataAnnotations;

namespace PMS.Domain.entity
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        // Personal Info
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }

        // Contact Info
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Phone { get; set; }

        // Identification
        public IdType IdType { get; set; } // Enum: Passport, DriversLicense, NationalID
        [Required, StringLength(50)]
        public string IdNumber { get; set; }

        // Address
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        // Emergency Contact
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }

        // Preferences
        public string GuestPreferences { get; set; }
      //  public bool IsVip { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        public ICollection<Reservation> Reservations { get; set; }

    }
}
