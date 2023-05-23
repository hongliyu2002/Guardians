# Guardians
```cs
public class Encryptor
{
    public static string EncryptData(string content, string privateKeyBase64, Encoding encode)
    {
        byte[] privateKeyBytes = Convert.FromBase64String(privateKeyBase64);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportPKCS8PrivateKey(privateKeyBytes, out _);

            byte[] contentBytes = encode.GetBytes(content);
            byte[] cipherBytes = rsa.Encrypt(contentBytes, RSAEncryptionPadding.Pkcs1);

            return Convert.ToBase64String(cipherBytes);
        }
    }

    public static string DecryptData(string encryptedStr, string publicKeyBase64, Encoding encode)
    {
        var publicKeyBytes = Convert.FromBase64String(publicKeyBase64);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);
            byte[] encryptedBytes = Convert.FromBase64String(encryptedStr);
            byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
            return encode.GetString(decryptedBytes);
        }
    }
}


[UsedImplicitly]
internal sealed class CaseApplicationServiceClient : HttpClientServiceBase, ICaseApplicationService
{
    public CaseApplicationServiceClient(string name, System.Net.Http.HttpClient httpClient, RemoteService options)
        : base(name, httpClient, options)
    {
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> CreateCaseAsync(CaseForCreationDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PostAsync("/api/cases", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> UpdateCaseInfoAsync(CaseId caseID, CaseForUpdateDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PutAsync($"/api/cases/{caseID}", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> ChangeCaseStatusAsync(CaseId caseID, CaseForStatusChangeDto input)
    {
        var content = input.AsJsonContent();
        var response = await HttpClient.PutAsync($"/api/cases/{caseID}/status", content);
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseId>> DeleteCaseAsync(CaseId caseID)
    {
        var response = await HttpClient.DeleteAsync($"/api/cases/{caseID}");
        var result = await response.Content.ReadAsAsync<ResultDto<CaseId>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<CaseDto>> GetCaseAsync(CaseId caseID)
    {
        var response = await HttpClient.GetAsync($"/api/cases/{caseID}");
        var result = await response.Content.ReadAsAsync<ResultDto<CaseDto>>();
        return result;
    }

    /// <inheritdoc />
    public async Task<ResultDto<PagedResultDto<CaseDto>>> ListPagedCasesAsync(string? reporterNo, DateTimeOffset startDate, DateTimeOffset endDate, int pageNo = 1, int pageSize = 10)
    {
        var query = new StringBuilder();
        query.Append($"?pageNo={pageNo}&pageSize={pageSize}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        if (reporterNo.IsNotNullOrEmpty())
        {
            query.Append($"&reporterNo={reporterNo}");
        }
        var response = await HttpClient.GetAsync($"/api/cases/{query}");
        var result = await response.Content.ReadAsAsync<ResultDto<PagedResultDto<CaseDto>>>();
        return result;
    }
}

```
