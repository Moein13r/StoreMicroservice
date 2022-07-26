using System.ComponentModel.DataAnnotations;

namespace Chat.Data.Dtos
{
    public class MessageCreateDto
    {
        [Required]
        public string message { get; set; } = null!;
        [Required]
        public int UserId { get; set; }        
        public int? ParentMessageId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
