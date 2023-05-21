using FluentValidation;
using JetBrains.Annotations;

namespace Guardians.Domain.Validations;

[UsedImplicitly]
public sealed class SceneValidator : AbstractValidator<Scene>
{
    /// <inheritdoc />
    public SceneValidator()
    {
        RuleFor(scene => scene.Title).NotEmpty().WithMessage("场景标题不能为空。").MaximumLength(100).WithMessage("场景标题不能超过100个字符。");
    }
}