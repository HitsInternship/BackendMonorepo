namespace AuthModel.Service.Service;

public static class AuthSettings
{
    public static string? PrivateKey = Environment.GetEnvironmentVariable("ACCESS_TOKEN_SECRET");
    public static DateTime GetTokenExpires()
    {
        return DateTime.UtcNow.AddDays(1);
    }
}