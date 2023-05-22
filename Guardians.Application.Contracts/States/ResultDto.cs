using JetBrains.Annotations;

namespace Guardians.Application.Contracts.States;

[PublicAPI]
public sealed class ResultDto<T>
{
    public int Code { get; set; }
    
    public string Message { get; set; } = default!;
    
    public T? Data { get; set; }
}