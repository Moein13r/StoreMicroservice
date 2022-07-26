using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class Message
    {
        public Message()
        {
            SeenedMessages = new HashSet<SeenedMessage>();
        }

        public int Id { get; set; }
        public string message { get; set; }
        public int UserId { get; set; }
        public int? ParentMessageId { get; set; }
        public int RoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual Room Room { get; set; }
        public virtual ICollection<SeenedMessage> SeenedMessages { get; set; }
    }
}
