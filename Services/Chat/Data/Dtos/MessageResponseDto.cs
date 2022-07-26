using System.ComponentModel.DataAnnotations;

namespace Chat.Data.Dtos
{
    public class MessageResponseDto
    {   
        public int Id { get; set; }          
        public string message { get; set; } = null!;        
        public int UserId { get; set; }        
        public int? ParentMessageId { get; set; }
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
