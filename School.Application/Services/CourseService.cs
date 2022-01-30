﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Application.Mapper;
using School.Application.Models;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class CourseService : IService<CourseModel>
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
        }

        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            var course = await _courseRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CourseModel>>(course);
            return mapped;
        }

        public async Task<CourseModel> GetById(int courseId)
        {
            var course = await _courseRepository.GetCourseByIdAsync(courseId);
            var mapped = ObjectMapper.Mapper.Map<CourseModel>(course);
            return mapped;
        }

        public async Task<IEnumerable<CourseModel>> GetBySearch(string searchTerm)
        {
            var courseList = await _courseRepository.GetCourseByNameAsync(searchTerm);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CourseModel>>(courseList);
            return mapped;
        }

        public Task<IEnumerable<CourseModel>> GetByParent(int parentId)
        {
            // not used for this model
            throw new NotImplementedException();
        }

        public Task Create(CourseModel model)
        {
            // not used for this model
            throw new NotImplementedException();
        }

        public Task Update(CourseModel model)
        {
            // not used for this model
            throw new NotImplementedException();
        }

        public Task Delete(CourseModel model)
        {
            // not used for this model
            throw new NotImplementedException();
        }
    }
}
