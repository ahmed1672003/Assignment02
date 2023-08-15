

namespace Assignment02.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<StudentSubject> StudentsSubjects { get; set; }
    public DbSet<Subject> Subjects { get; set; }
}
