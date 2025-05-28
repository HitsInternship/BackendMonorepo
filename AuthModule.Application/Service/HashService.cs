using System.Security.Cryptography;
using System.Text;
using AuthModel.Service.Interface;

namespace AuthModel.Service.Service;

public class HashService : IHashService
{
    public string GetHash(HashAlgorithm hashAlgorithm, string input)
    {
        byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        
        var sBuilder = new StringBuilder();

        foreach (var t in data)
        {
            sBuilder.Append(t.ToString("x2"));
        }
        
        return sBuilder.ToString();
    }
}