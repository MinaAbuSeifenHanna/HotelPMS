namespace PMS.Features.Reservations.Application.DTOS
{
    public class ReservationResultDto
    {
        public int Id { get; set; }

        public string GuestFullName { get; set; }
        public string GuestPhone { get; set; }
        public string GuestIdNumber { get; set; }

        public string RoomNumber { get; set; }
        public string RoomType { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public string Status { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal DepositAmount { get; set; }
        public decimal RemainingBalance { get; set; }

        public List<AddCompanionDto> Companions { get; set; }
    }
}
