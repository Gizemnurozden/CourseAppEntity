using System.ComponentModel.DataAnnotations;

namespace CourseAppEntity.Data
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime Start { get; set; }
        public ICollection<Course> Courses { get; set; } = new List<Course>();

    }
}