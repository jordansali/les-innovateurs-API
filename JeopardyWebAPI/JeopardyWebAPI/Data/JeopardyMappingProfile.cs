using AutoMapper;
using JeopardyWebAPI.Models;

namespace JeopardyWebAPI.Data
{
    public class JeopardyMappingProfile : Profile
    {
        public JeopardyMappingProfile()
        {
            CreateMap<Categories, CategoriesModel>()
                .ForMember(c => c.Questions, opt => opt.MapFrom(src => src.Questions)) //so the questions can be viewed from categories controller                
                .ReverseMap();

            CreateMap<Questions, QuestionsModel>()                
                .ForMember(q => q.Category, opt => opt.MapFrom(src => src.Category)) //so the categories can be viewed from questions controller
                .ForPath(q => q.Category.CategoryNameEn, opt => opt.MapFrom(src => src.Category.CategoryNameEn))
                .ReverseMap();

        }
    }
}
