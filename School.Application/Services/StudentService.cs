using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Core.Entities;
using School.Core.Interfaces;
using School.Core.Repositories;
using School.Application.Models;
using School.Application.Mapper;
using School.Application.Interfaces;

namespace School.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAppLogger<StudentService> _logger;

        public StudentService(IStudentRepository studentRepository, IAppLogger<StudentService> logger)
        {
            _studentRepository = studentRepository ?? throw new ArgumentNullException(nameof(studentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<StudentModel>> GetStudentList()
        {
            var studentList = await _studentRepository.GetStudentListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task<StudentModel> GetStudentById(int studentId)
        {
            var student = await _studentRepository.GetStudentByIdAsync(studentId);
            var mapped = ObjectMapper.Mapper.Map<StudentModel>(student);
            return mapped;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentByName(string name)
        {
            var studentList = await _studentRepository.GetStudentByNameAsync(name);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task<IEnumerable<StudentModel>> GetStudentByGroup(int groupId)
        {
            var studentList = await _studentRepository.GetStudentByGroupAsync(groupId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<StudentModel>>(studentList);
            return mapped;
        }

        public async Task<StudentModel> Create(StudentModel studentModel)
        {
            await ValidateStudentIfExist(studentModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Student>(studentModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Student could not be mapped.");

            var newEntity = await _studentRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Student successfully added - SchoolAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<StudentModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(StudentModel studentModel)
        {
            ValidateStudentIfNotExist(studentModel);

            var editStudent = await _studentRepository.GetByIdAsync(studentModel.Id);
            if (editStudent == null)
                throw new ApplicationException($"Student could not be loaded.");

            ObjectMapper.Mapper.Map<StudentModel, Student>(studentModel, editStudent);

            await _studentRepository.UpdateAsync(editStudent);
            _logger.LogInformation($"Student successfully updated - SchoolAppService");
        }

        public async Task Delete(StudentModel studentModel)
        {
            ValidateStudentIfNotExist(studentModel);
            var deletedStudent = await _studentRepository.GetByIdAsync(studentModel.Id);
            if (deletedStudent == null)
                throw new ApplicationException($"Student could not be loaded.");

            await _studentRepository.DeleteAsync(deletedStudent);
            _logger.LogInformation($"Student successfully deleted - SchoolService");
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
