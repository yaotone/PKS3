using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class Subject
{
    public int SubjectId { get; set; }

    public string? NameSubject { get; set; }

    public virtual ICollection<EnrolleeSubject> EnrolleeSubjects { get; set; } = new List<EnrolleeSubject>();

    public virtual ICollection<ProgramSubject> ProgramSubjects { get; set; } = new List<ProgramSubject>();
}
