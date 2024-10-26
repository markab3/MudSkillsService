using System;
using System.Collections.Generic;

namespace MudSkillsService.Models
{
    public partial class Skill
    {
        public Skill() { }

        public int SkillId { get; set; }
        public string SkillName { get; set; } = null!;
        public int MudId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Mud Mud { get; set; }
    }
}
