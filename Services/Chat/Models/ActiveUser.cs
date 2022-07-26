using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class ActiveUser
    {
        public int RoomUsersId { get; set; }
        public string ConnectionId { get; set; }

        public virtual RoomsUser RoomUsers { get; set; }
    }
}
