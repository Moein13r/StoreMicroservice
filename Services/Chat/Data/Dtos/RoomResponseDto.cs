using System.ComponentModel.DataAnnotations;

namespace Chat.Data.Dtos
{
    public class RoomResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public int UnSeenedMessages { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Type { get; set; }
    }
}
