using System;
using System.Collections.Generic;

namespace CompanyEFcore.Models;

public partial class Depenent
{
    public int DependentId { get; set; }

    public string DependentName { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public int EmpId { get; set; }

    public virtual Employee Emp { get; set; } = null!;
}
