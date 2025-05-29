using StudentModule.Domain.Entities;
using StudentModule.Domain.Enums;


namespace StudentModule.Contracts.DTOs
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Middlename { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public bool IsHeadMan { get; set; }
        public StudentStatus Status { get; set; }
        public StudentInternshipStatus InternshipStatus { get; set; }
        public int? GroupNumber { get; set; }
        public int? Course { get; set; }

        public StudentDto() { }

        public StudentDto(StudentEntity student)
        {
            Id = student.Id;
            Middlename = student.Middlename;
            Phone = student.Phone;
            Status = student.Status;
            InternshipStatus = student.InternshipStatus;
            IsHeadMan = student.IsHeadMan;
            Name = student.User.Name;
            Surname = student.User.Surname;
            Email = student.User.Email;
            GroupNumber = student.Group.GroupNumber;
            Course = student.Group.Stream.Course;
        }
    }
}