using AutoMapper;
using Guardians.Application.Contracts.States;
using Guardians.Domain;
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
           .ForMember(dest => dest.StatusCode, opt => opt.MapFrom(src => src.Status.Value))
           .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.Status.Name))
           .ReverseMap();
        CreateMap<Scene, SceneDto>().ReverseMap();
    }
}