using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ExaminationEF.Data
{
    public class ExaminationDbContextFactory : IDesignTimeDbContextFactory<ExaminationDbContext>
    {
        public ExaminationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ExaminationDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-B0IDBI8\\MSSQLSERVER342;Database=ExaminationDB;Trusted_Connection=True;TrustServerCertificate=True;");

            return new ExaminationDbContext(optionsBuilder.Options);
        }
    }
}

