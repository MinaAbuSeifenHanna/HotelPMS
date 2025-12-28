namespace PMS.Features.Guests.Application.DTOS
{
    public class GuestResultDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}"; // for read
        public string Email { get; set; }
        public string Phone { get; set; }
        public string IdNumber { get; set; }
        public string Nationality { get; set; }
       // public bool IsVip { get; set; }
    }
}
