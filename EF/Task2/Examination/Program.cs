using ExaminationEF.Data;
using Microsoft.EntityFrameworkCore;

namespace ExaminationEF
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            var optionsBuilder = new DbContextOptionsBuilder<ExaminationDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-B0IDBI8\\MSSQLSERVER342;Database=ExaminationDB;Trusted_Connection=True;TrustServerCertificate=True;");

            using var context = new ExaminationDbContext(optionsBuilder.Options);

            Console.WriteLine("DbContext is ready ✅");

        //    if (context.Database.CanConnect())
        //    {
        //        Console.WriteLine("Connected to DB successfully ");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed to connect to DB ");
        //    }
        }
    }
}

