using System.ComponentModel.DataAnnotations;

namespace CourseAppEntity.Data
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public string? StudentLastName { get; set; }

        public string? NameLastName { get {
            return this.StudentName + " " + this.StudentLastName;
        }}
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public ICollection<CourseRegistration> CourseRegistrations { get; set; } = new List<CourseRegistration>();
    }
}