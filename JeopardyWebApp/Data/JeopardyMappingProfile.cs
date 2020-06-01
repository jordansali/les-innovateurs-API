using AutoMapper;
using JeopardyWebApp.Data.Entities;
using JeopardyWebApp.Models;

namespace JeopardyWebApp.Data
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

            // TODO: Players
        }
    }
}
