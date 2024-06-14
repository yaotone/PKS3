using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class EnrolleeSubject
{
    public int EnrolleeSubjectId { get; set; }

    public int? EnrolleeId { get; set; }

    public int? SubjectId { get; set; }

    public int? Result { get; set; }

    public virtual Enrollee? Enrollee { get; set; }

    public virtual Subject? Subject { get; set; }
}
