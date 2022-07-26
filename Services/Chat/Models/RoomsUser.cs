using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class RoomsUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }
    }
}
