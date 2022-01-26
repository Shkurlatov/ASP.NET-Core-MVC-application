using School.Service.Interfaces;
using School.Service.Models;
using School.Web.Interfaces;
using School.Web.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Services
{
    public class CoursePageService : ICoursePageService
    {
        private readonly ICourseService _courseAppService;
        private readonly IMapper _mapper;

        public CoursePageService(ICourseService courseAppService, IMapper mapper)
        {
            _courseAppService = courseAppService ?? throw new ArgumentNullException(nameof(courseAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<CourseViewModel>> GetCourses(string name, int courseId)
        {
            if (courseId != 0)
            {
                var course = await _courseAppService.GetCourseById(courseId);
                var list = new List<CourseModel> { course };
                var mapped = _mapper.Map<IEnumerable<CourseViewModel>>(list);
                return mapped;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                var list = await _courseAppService.GetCourseList();
                var mapped = _mapper.Map<IEnumerable<CourseViewModel>>(list);
                return mapped;
            }

            var listByName = await _courseAppService.GetCourseByName(name);
            var mappedByName = _mapper.Map<IEnumerable<CourseViewModel>>(listByName);
            return mappedByName;
        }
    }
}
