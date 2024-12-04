using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CourseAppEntity.Data
{
    //connection with db
    public class CourseContext: DbContext
    {
        public  CourseContext(DbContextOptions<CourseContext>  options) : base(options)
        {

        }
        //entity entegred 
        public DbSet<Course> Courses => Set<Course>();
         public DbSet<Student> Students => Set<Student>();
         public DbSet<CourseRegistration> CourseRegistrations => Set<CourseRegistration>();
         public DbSet<Teacher> Teachers => Set<Teacher>();
        
    }

    //code-first =entity, dbcontext => database(sqlite)
}