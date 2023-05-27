using AutoMapper;
using Guardians.Application.Contracts.States;
using Guardians.Domain;
using Guardians.Domain.Shared;
using JetBrains.Annotations;

namespace Guardians.Application.Contributors;

[UsedImplicitly]
internal sealed class MappingProfile : Profile
{
    /// <inheritdoc />
    public MappingProfile()
    {
        CreateMap<Case, CaseDto>()
           .ForMember(dest => dest.SceneTitle, opt => opt.MapFrom(src => src.Scene.Title))
           .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.Status))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status == CaseStatus.Reviewing ? "待核实" : src.Status == CaseStatus.Processing ? "处理中" : "已完结"));
        CreateMap<Scene, SceneDto>().ReverseMap();
    }
}