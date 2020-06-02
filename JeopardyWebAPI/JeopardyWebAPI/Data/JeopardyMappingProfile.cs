using AutoMapper;
using JeopardyWebAPI.Models;

namespace JeopardyWebAPI.Data
{
    public class JeopardyMappingProfile : Profile
    {
        public JeopardyMappingProfile()
        {
            CreateMap<Categories, CategoriesModel>()
                .ForMember(c => c.Questions, opt => opt.MapFrom(src => src.Questions)) //so the questions can be viewed from categories
                .ReverseMap();

            CreateMap<Questions, QuestionsModel>()
                //.ForMember(q => q.Category, opt => opt.Ignore()) //don't overwrite category 
                .ForMember(q => q.Category, opt => opt.MapFrom(src => src.Category)) //so the categories can be viewed from questions
                .ReverseMap();




        }
    }
}
