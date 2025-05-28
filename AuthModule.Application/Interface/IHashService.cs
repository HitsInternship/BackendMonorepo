using System.Security.Cryptography;

namespace AuthModel.Service.Interface;

public interface IHashService
{
    public string GetHash(HashAlgorithm hashAlgorithm, string input);
}