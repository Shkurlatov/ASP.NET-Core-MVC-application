using School.Application.Models.Studies;
using School.Application.Models.Users;
using School.Domain.Entities.Studies;
using School.Domain.Entities.Users;
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
            CreateMap<Student, StudentModel>().ReverseMap();
            CreateMap<Group, GroupModel>().ReverseMap();
            CreateMap<Course, CourseModel>().ReverseMap();
            CreateMap<Curator, CuratorModel>().ReverseMap();
        }
    }
}
