using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
[Serializable]
public sealed class EncryptedQueryDto
{
    public string Param { get; set; } = string.Empty;
}