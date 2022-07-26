using System;
using System.Collections.Generic;

namespace Chat.Models
{
    public partial class Room
    {
        public Room()
        {
            Messages = new HashSet<Message>();
            RoomsUsers = new HashSet<RoomsUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Type { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<RoomsUser> RoomsUsers { get; set; }
    }
}
