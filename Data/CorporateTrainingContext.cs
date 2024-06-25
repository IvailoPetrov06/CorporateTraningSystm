using CorporateTraningSystm.Model;
using Microsoft.EntityFrameworkCore;

namespace CorporateTraningSystm.Data
{
    public class CorporateTrainingContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        public CorporateTrainingContext(DbContextOptions<CorporateTrainingContext> options)
            : base(options)
        {
        }
    }
}
