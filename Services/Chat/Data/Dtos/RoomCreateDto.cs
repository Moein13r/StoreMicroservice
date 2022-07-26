using System.ComponentModel.DataAnnotations;

namespace Chat.Data.Dtos
{
    public class RoomCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
