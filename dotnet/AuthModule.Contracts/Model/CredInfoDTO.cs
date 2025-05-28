namespace AuthModule.Contracts.Model;

public class CredInfoDTO
{
    public Guid UserId { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}