using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Application.Mapper;
using School.Application.Models.Studies;
using School.Domain.Entities.Studies;
using School.Domain.Interfaces.Studies;

namespace School.Application.Services.Studies
{
    public class CourseService : IStudyService<CourseModel>
    {
        private readonly IStudyRepository<Course> _courseRepository;

        public CourseService(IStudyRepository<Course> courseRepository)
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
            var course = await _courseRepository.GetByIdAsync(courseId);
            var mapped = ObjectMapper.Mapper.Map<CourseModel>(course);
            return mapped;
        }

        public async Task<IEnumerable<CourseModel>> GetBySearch(string searchTerm)
        {
            var courseList = await _courseRepository.GetBySearchAsync(searchTerm);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CourseModel>>(courseList);
            return mapped;
        }

        public Task<IEnumerable<CourseModel>> GetByParent(int parentId)
        {
            // not used for this model
            throw new NotImplementedException();
        }

        public async Task Create(CourseModel courseModel)
        {
            await ValidateGroupIfExist(courseModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Course>(courseModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Group could not be mapped.");

            await _courseRepository.AddAsync(mappedEntity);
        }

        public async Task Update(CourseModel courseModel)
        {
            var editGroup = await _courseRepository.GetByIdAsync(courseModel.Id);
            if (editGroup == null)
                throw new ApplicationException($"Group could not be loaded.");

            editGroup.Name = courseModel.Name;
            editGroup.Description = courseModel.Description;

            await _courseRepository.UpdateAsync(editGroup);
        }

        public async Task Delete(CourseModel courseModel)
        {
            var deletedCourse = await _courseRepository.GetByIdAsync(courseModel.Id);
            if (deletedCourse == null)
                throw new ApplicationException($"Group could not be loaded.");

            await _courseRepository.DeleteAsync(deletedCourse);
        }

        private async Task ValidateGroupIfExist(CourseModel courseModel)
        {
            var existingEntity = await _courseRepository.GetByIdAsync(courseModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{courseModel.ToString()} with this id already exists");
        }
    }
}
