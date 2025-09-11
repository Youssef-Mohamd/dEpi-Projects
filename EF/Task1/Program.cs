using CompanyEFcore.Models;

namespace CompanyEFcore
{
    internal class Program
    {
        static void Main(string[] args)
        {


            using (var context = new CompanyContext())
            {
                var emp = new Employee
                {
                    EmpId = 9999,
                    FirstName = "Youssef",
                    LastName = "Mohamed",
                    Gender = "M",
                    DeptId = 1
                };

                context.Employees.Add(emp);
                context.SaveChanges();
                Console.WriteLine("Employee Added!");
            }


            using (var context = new CompanyContext())
            {
                var employees = context.Employees
                    .Select(e => new
                    {
                        e.EmpId,
                        FullName = e.FirstName + " " + e.LastName,
                        e.Gender,
                        e.BirthDate,
                        DepartmentName = e.Dept.DeptName
                    })
                    .ToList();

                foreach (var emp in employees)
                {
                    Console.WriteLine($"{emp.EmpId} - {emp.FullName} - {emp.Gender} - {emp.DepartmentName}");
                }
            }



        }
    }
}
