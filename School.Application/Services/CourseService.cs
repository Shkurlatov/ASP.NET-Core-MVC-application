using School.Application.Mapper;
using School.Application.Interfaces;
using School.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Core.Repositories;
using School.Application.Models;

namespace School.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IAppLogger<CourseService> _logger;

        public CourseService(ICourseRepository courseRepository, IAppLogger<CourseService> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<CourseModel> GetCourseById(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            var mapped = ObjectMapper.Mapper.Map<CourseModel>(course);
            return mapped;
        }

        public async Task<IEnumerable<CourseModel>> GetCourseList()
        {
            var course = await _courseRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CourseModel>>(course);
            return mapped;
        }

        public async Task<IEnumerable<CourseModel>> GetCourseByName(string name)
        {
            var courseList = await _courseRepository.GetCourseByNameAsync(name);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CourseModel>>(courseList);
            return mapped;
        }
    }
}
