using School.Application.Models;
using School.Core.Entities;
using AutoMapper;
using System;

namespace School.Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<SchoolDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class SchoolDtoMapper : Profile
    {
        public SchoolDtoMapper()
        {
            CreateMap<Student, StudentModel>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName)).ReverseMap();

            CreateMap<Group, GroupModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<Course, CourseModel>().ReverseMap();
        }
    }
}
