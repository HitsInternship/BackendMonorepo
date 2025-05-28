namespace AuthModule.Contracts.Model;

public class EditPasswordDTO
{
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string Login { get; set; }
}