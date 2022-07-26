using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class SeenedMessage
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MessageId { get; set; }

        public virtual Message Message { get; set; }
    }
}
