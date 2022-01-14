using AutoMapper;
using EO = StackOverflow.Platform.Entities;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Platform.Profiles
{
    public class PlatformProfile : Profile
    {
        public PlatformProfile()
        {
            CreateMap<EO.Question, BO.Question>().ReverseMap();
            CreateMap<EO.Comment, BO.Comment>().ReverseMap();
        }
    }
}
