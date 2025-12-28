using PMS.Domain.Enums.GuestEnums;

namespace PMS.Application.Features.Guests.DTOs
{
    public class AddGuestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public IdType IdType { get; set; }
        public IdType IdType { get; set; }
        public string IdNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string GuestPreferences { get; set; }
        //public bool IsVip { get; set; }
    }
}



