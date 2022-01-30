using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Application.Mapper;
using School.Application.Models;
using School.Application.Repositories;
using School.Domain.Entities;

namespace School.Application.Services
{
    public class StudentService : IService<StudentModel>
    {
        private readonly Repository<Student> _studentRepository;

        public StudentService(Repository<Student> studentRepository)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
        }

        public async Task<IEnumerable<StudentModel>> GetAll()
        {
            var studentList = await _studentRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task<StudentModel> GetById(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var mapped = ObjectMapper.Mapper.Map<StudentModel>(student);
            return mapped;
        }

        public async Task<IEnumerable<StudentModel>> GetBySearch(string searchTerm)
        {
            var studentList = await _studentRepository.GetBySearchAsync(searchTerm);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task<IEnumerable<StudentModel>> GetByParent(int parentId)
        {
            var studentList = await _studentRepository.GetByParentAsync(parentId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task Create(StudentModel studentModel)
        {
            await ValidateStudentIfExist(studentModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Student>(studentModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Student could not be mapped.");

            await _studentRepository.AddAsync(mappedEntity);
        }

        public async Task Update(StudentModel studentModel)
        {
            ValidateStudentIfNotExist(studentModel);

            var editStudent = await _studentRepository.GetByIdAsync(studentModel.Id);
            if (editStudent == null)
                throw new ApplicationException($"Student could not be loaded.");

            ObjectMapper.Mapper.Map<StudentModel, Student>(studentModel, editStudent);

            await _studentRepository.UpdateAsync(editStudent);
        }

        public async Task Delete(StudentModel studentModel)
        {
            ValidateStudentIfNotExist(studentModel);

            var deletedStudent = await _studentRepository.GetByIdAsync(studentModel.Id);
            if (deletedStudent == null)
                throw new ApplicationException($"Student could not be loaded.");

            await _studentRepository.DeleteAsync(deletedStudent);
        }

        private async Task ValidateStudentIfExist(StudentModel studentModel)
        {
            var existingEntity = await _studentRepository.GetByIdAsync(studentModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{studentModel.ToString()} with this id already exists");
        }

        private void ValidateStudentIfNotExist(StudentModel studentModel)
        {
            var existingEntity = _studentRepository.GetByIdAsync(studentModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{studentModel.ToString()} with this id is not exists");
        }
    }
}
