using AutoMapper;
using Edtech_backend_API.DTOs;
using Edtech_backend_API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.StandaryDictionary
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Language, LanguageDto>().ReverseMap();
            CreateMap<Level, LevelDto>().ReverseMap();
            CreateMap<CourseInstructor, CourseInstructorDto>().ReverseMap();
            CreateMap<CourseReview, CourseReviewDto>().ReverseMap();
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<CourseVideo, CourseVideoDto>().ReverseMap();
        }
    }
}
