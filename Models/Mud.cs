using System;
using System.Collections.Generic;

namespace MudSkillsService.Models
{
    public partial class Mud
    {
        public Mud()
        {
            Players = new HashSet<Player>();
            Skills = new HashSet<Skill>();
        }

        public int MudId { get; set; }
        public string MudName { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual ICollection<Player> Players { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
    }
}
