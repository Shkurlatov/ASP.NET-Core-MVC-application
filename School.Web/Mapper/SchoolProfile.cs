using School.Application.Models;
using School.Web.ViewModels;
using AutoMapper;

namespace School.Web.Mapper
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<StudentModel, StudentViewModel>().ReverseMap();
            CreateMap<GroupModel, GroupViewModel>().ReverseMap();
            CreateMap<CourseModel, CourseViewModel>().ReverseMap();
        }
    }
}
