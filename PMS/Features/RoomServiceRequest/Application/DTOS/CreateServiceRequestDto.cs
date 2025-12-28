using PMS.Features.RoomServiceRequests.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PMS.Features.RoomServiceRequests.Application.DTOS
{
    public class CreateServiceRequestDto
    {
        [Required(ErrorMessage = "رقم الغرفة مطلوب")]
        public int RoomId { get; set; }

        [Required(ErrorMessage = "نوع الخدمة مطلوب")]
        public string ServiceType { get; set; }

        [Required(ErrorMessage = "مستوى الأهمية مطلوب")]
        public ServicePriority Priority { get; set; } 

        [Required(ErrorMessage = "الوصف مطلوب")]
        [StringLength(500, ErrorMessage = "الوصف لا يمكن أن يتجاوز 500 حرف")]
        public string Description { get; set; }
    }
}
