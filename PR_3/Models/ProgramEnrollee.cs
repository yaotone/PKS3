using System;
using System.Collections.Generic;

namespace PR_3.Models;

public partial class ProgramEnrollee
{
    public int ProgramEnrolleeId { get; set; }

    public int? ProgramId { get; set; }

    public int? EnrolleeId { get; set; }

    public virtual Enrollee? Enrollee { get; set; }

    public virtual Program? Program { get; set; }
}
