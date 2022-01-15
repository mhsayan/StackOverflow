using AutoMapper;
using StackOverflow.Membership.Entities;
using StackOverflow.Web.Models.Question;
using BO = StackOverflow.Platform.BusinessObjects;

namespace StackOverflow.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateQuestionModel, BO.Question>().ReverseMap();
            CreateMap<QuestionDetailsModel, BO.Question>().ReverseMap();
        }
    }
}