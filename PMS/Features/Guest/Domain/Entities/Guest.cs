using PMS.Features.Guests.Domain.Enums;
using PMS.Features.Reservations.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.Guests.Domain.Entities
{
    public class Guest
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string FirstName { get; set; }

        [Required, StringLength(50)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // خلي الجنسية اختيارية لو مش عايز تجبر اليوزر يكتبها
        public string? Nationality { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string Phone { get; set; }

        public IdType IdType { get; set; }

        [Required, StringLength(50)]
        public string IdNumber { get; set; }

        // الحقول دي يفضل تكون Nullable عشان الـ SQL ميضربش
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; } // هنا كان الخطأ الأول

        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }

        public string? GuestPreferences { get; set; } // وهنا كان الخطأ الثاني

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}