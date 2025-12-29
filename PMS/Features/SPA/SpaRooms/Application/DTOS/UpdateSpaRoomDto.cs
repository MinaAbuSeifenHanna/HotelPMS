using System;
using System.ComponentModel.DataAnnotations;
using PMS.Features.SPA.SpaRooms.Domain.Enums;

namespace PMS.Features.SPA.SpaRooms.Application.DTOS;

public class UpdateSpaRoomDto
{
        [Required, StringLength(50)]
        public string RoomName { get; set; } = null!;

        [Required]
        public SpaRoomType RoomType { get; set; }

        public bool IsAvailable { get; set; }
}
