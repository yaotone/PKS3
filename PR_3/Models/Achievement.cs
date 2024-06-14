using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class Achievement
{
    public int AchievementId { get; set; }

    public string? NameAchievement { get; set; }

    public int Bonus { get; set; }

    public virtual ICollection<EnrolleeAchievement> EnrolleeAchievements { get; set; } = new List<EnrolleeAchievement>();
}
