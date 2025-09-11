using System;
using System.Collections.Generic;

namespace CompanyEFcore.Models;

public partial class Employee
{
    public int EmpId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public int DeptId { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Depenent> Depenents { get; set; } = new List<Depenent>();

    public virtual Department Dept { get; set; } = null!;
}
