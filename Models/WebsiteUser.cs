using System;
using System.Collections.Generic;

namespace MudSkillsService.Models
{
    public partial class WebsiteUser
    {
        public WebsiteUser()
        {
            Players = new HashSet<Player>();
        }

        public int WebsiteUserId { get; set; }
        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
