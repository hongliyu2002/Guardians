using System.Text;
using Org.BouncyCastle.Security;

namespace Guardians.Blazor;

public static class Encryptor
{
    public const string DailyPublicKeyBase64 =
        "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCfS3rnByQ8QV3ayiwFZHP6aNxEDS92tU1W1VPQLSh7CsIlyx1GiuUjZeXbdMI7ylLBDWY7QM2hFNZDOQzHS8zzUx3f8Ayiy83BiJ+uM1JotprROP6QmP9VafapxdHjbuDQOZHclcbRxfwWME5dilKQSg2ygNoyHHg8F3Rt15OzOQIDAQAB";
    public const string ProductPublicKeyBase64 =
        "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC0FuzZpEBYTh1QKL+SM83qXYNLDx/V8u6QaDKG4i015hAhEMrb9ab1d9uStBF1jRwJMHPggWtIZSEio27ik2ONJuIpYSK5TiAiXjmWAXwc9r5VSTSwjK2IN0bw8N/Z0+JgObc91i4pApt9Dd5eqZOiN2qbDh1knDfnzA3OFsdyYwIDAQAB";

    public static string DecryptData(string encryptedContent, string publicKeyBase64, Encoding encoding)
    {
        try
        {
            var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);
            var publicKey = PublicKeyFactory.CreateKey(publicKeyBytes);
            var cipher = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
            cipher.Init(false, publicKey);
            var encryptedBytes = Convert.FromBase64String(encryptedContent);
            var plainText = cipher.DoFinal(encryptedBytes);
            return encoding.GetString(plainText);
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }
}