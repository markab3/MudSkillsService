using System;
using System.Collections.Generic;

namespace MudSkillsService.Models
{
    public partial class PlayerSkill
    {
        public int PlayerSkillId { get; set; }
        public int PlayerId { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
        public int Bonus { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public DateTime? DateDeleted { get; set; }

        public virtual Player Player { get; set; } = null!;
        public virtual Skill Skill { get; set; } = null!;
    }
}
