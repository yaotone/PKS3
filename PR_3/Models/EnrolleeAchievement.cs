using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class EnrolleeAchievement
{
    public int EnrolleeAchievId { get; set; }

    public int? EnrolleeId { get; set; }

    public int AchievementId { get; set; }

    public virtual Achievement? Achievement { get; set; }

    public virtual Enrollee? Enrollee { get; set; }
}
