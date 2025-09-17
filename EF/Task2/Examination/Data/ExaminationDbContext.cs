using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExaminationEF.Models;
using Microsoft.EntityFrameworkCore;

namespace ExaminationEF.Data
{
    public class ExaminationDbContext : DbContext

    {


        public ExaminationDbContext(DbContextOptions<ExaminationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=ExaminationDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; } 
        public DbSet<MultipleChoiceQuestion> MultipleChoiceQuestions { get; set; }
        public DbSet<TrueFalseQuestion> TrueFalseQuestions { get; set; }
        public DbSet<EssayQuestion> EssayQuestions { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<InstructorCourse> InstructorCourses { get; set; }
        public DbSet<ExamAttempt> ExamAttempts { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            ConfigureEntities(modelBuilder);
            SeedInitialData(modelBuilder);

        }

        private void ConfigureEntities(ModelBuilder modelBuilder)
        {
            // Composite keys
            modelBuilder.Entity<StudentCourse>().HasKey(sc => new { sc.StudentId, sc.CourseId });
            modelBuilder.Entity<InstructorCourse>().HasKey(ic => new { ic.InstructorId, ic.CourseId });

            // Unique constraints (indexes)
            modelBuilder.Entity<Student>().HasIndex(s => s.Email).IsUnique();
            modelBuilder.Entity<Student>().HasIndex(s => s.StudentNumber).IsUnique();
            modelBuilder.Entity<Instructor>().HasIndex(i => i.Email).IsUnique();

            // Indexes for performance
            modelBuilder.Entity<Exam>().HasIndex(e => e.StartDate);
            modelBuilder.Entity<ExamAttempt>().HasIndex(a => a.StartTime);

            // Decimal precision
            modelBuilder.Entity<Course>().Property(c => c.MaximumDegree).HasPrecision(18, 2);
            modelBuilder.Entity<Question>().Property(q => q.Marks).HasPrecision(18, 2);
            modelBuilder.Entity<Exam>().Property(e => e.TotalMarks).HasPrecision(18, 2);
            modelBuilder.Entity<StudentCourse>().Property(sc => sc.Grade).HasPrecision(18, 2);
            modelBuilder.Entity<ExamAttempt>().Property(a => a.TotalScore).HasPrecision(18, 2);
            modelBuilder.Entity<StudentAnswer>().Property(sa => sa.MarksObtained).HasPrecision(18, 2);

            // Check constraints
           // modelBuilder.Entity<Exam>().HasCheckConstraint("CK_Exam_Dates", "[EndDate] > [StartDate]");
            //modelBuilder.Entity<Question>().HasCheckConstraint("CK_Question_Marks", "[Marks] > 0");
            //modelBuilder.Entity<Course>().HasCheckConstraint("CK_Course_MaximumDegree", "[MaximumDegree] > 0");

            // Relationships and delete behaviors
            modelBuilder.Entity<Course>()
                .HasMany(c => c.Exams)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // restrict if exams exist

            modelBuilder.Entity<Exam>()
                .HasMany(e => e.Questions)
                .WithOne(q => q.Exam)
                .HasForeignKey(q => q.ExamId)
                .OnDelete(DeleteBehavior.Cascade); // exam -> questions cascade

            modelBuilder.Entity<ExamAttempt>()
                .HasMany(a => a.StudentAnswers)
                .WithOne(sa => sa.ExamAttempt)
                .HasForeignKey(sa => sa.ExamAttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.ExamAttempts)
                .WithOne(a => a.Student)
                .HasForeignKey(a => a.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentAnswer>()
    .HasOne(sa => sa.Question)
    .WithMany(q => q.StudentAnswers)
    .HasForeignKey(sa => sa.QuestionId)
    .OnDelete(DeleteBehavior.Restrict);


            // TPH inheritance configuration (use QuestionType enum as discriminator)
            modelBuilder.Entity<Question>()
                .HasDiscriminator(q => q.QuestionType)
                .HasValue<MultipleChoiceQuestion>(QuestionType.MultipleChoice)
                .HasValue<TrueFalseQuestion>(QuestionType.TrueFalse)
                .HasValue<EssayQuestion>(QuestionType.Essay);

            // Configure string lengths etc (example)
            modelBuilder.Entity<Course>().Property(c => c.Title).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Course>().Property(c => c.Description).HasMaxLength(1000);

            // Student fields
            modelBuilder.Entity<Student>().Property(s => s.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Student>().Property(s => s.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Student>().Property(s => s.StudentNumber).IsRequired().HasMaxLength(20);

            // Instructor
            modelBuilder.Entity<Instructor>().Property(i => i.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Entity<Instructor>().Property(i => i.Email).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Instructor>().Property(i => i.Specialization).IsRequired().HasMaxLength(150);

            // QuestionText length
            modelBuilder.Entity<Question>().Property(q => q.QuestionText).IsRequired().HasMaxLength(1000);

            // MCQ options max 500
            modelBuilder.Entity<MultipleChoiceQuestion>().Property(m => m.OptionA).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<MultipleChoiceQuestion>().Property(m => m.OptionB).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<MultipleChoiceQuestion>().Property(m => m.OptionC).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<MultipleChoiceQuestion>().Property(m => m.OptionD).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<MultipleChoiceQuestion>().Property(m => m.CorrectOption).IsRequired().HasMaxLength(1);
        }

        private void SeedInitialData(ModelBuilder modelBuilder)
        {
            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Programming 101", Description = "Intro to programming", MaximumDegree = 100, CreatedDate = new DateTime(2024, 01, 01), IsActive = true },
                new Course { Id = 2, Title = "Databases", Description = "SQL & EF Core", MaximumDegree = 100, CreatedDate = new DateTime(2024, 02, 01), IsActive = true },
                new Course { Id = 3, Title = "Algorithms", Description = "Intro to algorithms", MaximumDegree = 100, CreatedDate = new DateTime(2024, 03, 01), IsActive = true }
            );

            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Student One", Email = "s1@example.com", StudentNumber = "S1001", EnrollmentDate = new DateTime(2023, 08, 01), IsActive = true },
                new Student { Id = 2, Name = "Student Two", Email = "s2@example.com", StudentNumber = "S1002", EnrollmentDate = new DateTime(2023, 09, 01), IsActive = true },
                new Student { Id = 3, Name = "Student Three", Email = "s3@example.com", StudentNumber = "S1003", EnrollmentDate = new DateTime(2023, 10, 01), IsActive = true },
                new Student { Id = 4, Name = "Student Four", Email = "s4@example.com", StudentNumber = "S1004", EnrollmentDate = new DateTime(2023, 11, 01), IsActive = true },
                new Student { Id = 5, Name = "Student Five", Email = "s5@example.com", StudentNumber = "S1005", EnrollmentDate = new DateTime(2023, 12, 01), IsActive = true }
            );

            // Seed Instructors
            modelBuilder.Entity<Instructor>().HasData(
                new Instructor { Id = 1, Name = "Instructor A", Email = "insA@example.com", Specialization = "Databases", HireDate = new DateTime(2022, 01, 01), IsActive = true },
                new Instructor { Id = 2, Name = "Instructor B", Email = "insB@example.com", Specialization = "Algorithms", HireDate = new DateTime(2023, 01, 01), IsActive = true }
            );

            // Seed Exams
            modelBuilder.Entity<Exam>().HasData(
                new Exam { Id = 1, Title = "Midterm DB", Description = "Database midterm", TotalMarks = 100, Duration = new TimeSpan(1, 30, 0), StartDate = new DateTime(2024, 05, 10, 09, 00, 00), EndDate = new DateTime(2024, 05, 10, 11, 00, 00), IsActive = true, CourseId = 2, InstructorId = 1 },
                new Exam { Id = 2, Title = "Algorithms Quiz", Description = "Quick quiz", TotalMarks = 50, Duration = new TimeSpan(0, 45, 0), StartDate = new DateTime(2024, 06, 01, 10, 00, 00), EndDate = new DateTime(2024, 06, 01, 10, 45, 00), IsActive = true, CourseId = 3, InstructorId = 2 }
            );

            // Seed Questions
            modelBuilder.Entity<MultipleChoiceQuestion>().HasData(
                new
                {
                    Id = 1,
                    QuestionText = "What is SQL?",
                    Marks = 5m,
                    QuestionType = QuestionType.MultipleChoice,
                    CreatedDate = new DateTime(2024, 01, 15),
                    ExamId = 1,
                    OptionA = "Query language",
                    OptionB = "Programming language",
                    OptionC = "Operating system",
                    OptionD = "Text editor",
                    CorrectOption = "A"
                }
            );
        }


        // (you can add more seeded data as needed)
    }
}

