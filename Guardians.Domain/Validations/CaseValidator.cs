using FluentValidation;
using JetBrains.Annotations;

namespace Guardians.Domain.Validations;

[UsedImplicitly]
public sealed class CaseValidator : AbstractValidator<Case>
{
    public CaseValidator()
    {
        RuleFor(caseObj => caseObj.SceneID).NotEmpty().WithMessage("场景编号不能为空。");
        RuleFor(caseObj => caseObj.Description).MaximumLength(500).WithMessage("描述不能超过500个字符。");
        RuleFor(caseObj => caseObj.Address).MaximumLength(200).WithMessage("地址不能超过200个字符。");
        RuleFor(caseObj => caseObj.PhotoUrl).MaximumLength(500).WithMessage("照片URL不能超过500个字符。");
        RuleFor(caseObj => caseObj.ReportedAt).NotEmpty().WithMessage("上报时间不能为空。");
        RuleFor(caseObj => caseObj.ReporterNo).NotEmpty().WithMessage("上报人编号不能为空。");
        RuleFor(caseObj => caseObj.ReporterName).NotEmpty().WithMessage("上报人姓名不能为空。").MaximumLength(100).WithMessage("上报人姓名不能超过50个字符。");
        RuleFor(caseObj => caseObj.ReporterMobile).MaximumLength(20).WithMessage("上报人手机号不能超过20个字符。");
    }
}