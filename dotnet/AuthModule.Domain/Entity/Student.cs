namespace AuthModule.Domain.Entity;

public class Student
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FIO { get; set; }
    public string Email { get; set; }
    public string Group { get; set; }
}