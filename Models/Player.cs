using System;
using System.Collections.Generic;

namespace MudSkillsService.Models
{
    public partial class Player
    {
        public Player()
        {
            PlayerSkills = new HashSet<PlayerSkill>();
        }

        public int PlayerId { get; set; }
        public string PlayerName { get; set; } = null!;
        public int MudId { get; set; }
        public int WebsiteUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Mud Mud { get; set; } = null!;
        public virtual WebsiteUser WebsiteUser { get; set; } = null!;
        public virtual ICollection<PlayerSkill> PlayerSkills { get; set; }
    }
}
