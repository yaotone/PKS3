using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class Enrollee
{
    public int EnrolleeId { get; set; }

    public string? NameEnrollee { get; set; }

    public virtual ICollection<EnrolleeAchievement> EnrolleeAchievements { get; set; } = new List<EnrolleeAchievement>();

    public virtual ICollection<EnrolleeSubject> EnrolleeSubjects { get; set; } = new List<EnrolleeSubject>();

    public virtual ICollection<ProgramEnrollee> ProgramEnrollees { get; set; } = new List<ProgramEnrollee>();
}
