using AutoMapper;
using JeopardyWebAPI.Data;
using JeopardyWebAPI.Models;

namespace JeopardyWebAPI.Data
{
    public class JeopardyMappingProfile : Profile
    {
        public JeopardyMappingProfile()
        {
            CreateMap<Categories, CategoriesModel>()
                .ReverseMap();

            CreateMap<Questions, QuestionsModel>()
                .ForMember(q => q.Category, opt => opt.Ignore()) //don't overwrite category   
                .ReverseMap();

        }
    }
}
