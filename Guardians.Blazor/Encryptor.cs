using System.Security.Cryptography;
using System.Text;

namespace Guardians.Blazor;

public static class Encryptor
{
    public static string DecryptData(string encryptedContent, string publicKeyBase64, Encoding encoding)
    {
        var publicKey = Convert.FromBase64String(publicKeyBase64);
        using var rsa = new RSACryptoServiceProvider();
        rsa.ImportSubjectPublicKeyInfo(new ReadOnlySpan<byte>(publicKey), out _);
        var encryptedData = Convert.FromBase64String(encryptedContent);
        var decryptedData = rsa.Decrypt(encryptedData, RSAEncryptionPadding.Pkcs1);
        return encoding.GetString(decryptedData);
    }
}