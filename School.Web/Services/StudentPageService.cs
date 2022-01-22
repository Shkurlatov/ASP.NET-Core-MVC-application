using School.Application.Interfaces;
using School.Application.Models;
using School.Web.Interfaces;
using School.Web.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Web.Services
{
    public class StudentPageService : IStudentPageService
    {
        private readonly IStudentService _studentAppService;
        private readonly IGroupService _groupAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentPageService> _logger;

        public StudentPageService(IStudentService studentAppService, IGroupService groupAppService, IMapper mapper, ILogger<StudentPageService> logger)
        {
            _studentAppService = studentAppService ?? throw new ArgumentNullException(nameof(studentAppService));
            _groupAppService = groupAppService ?? throw new ArgumentNullException(nameof(groupAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudents(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var list = await _studentAppService.GetStudentList();
                var mapped = _mapper.Map<IEnumerable<StudentViewModel>>(list);
                return mapped;
            }

            var listByLastName = await _studentAppService.GetStudentByName(name);
            var mappedByName = _mapper.Map<IEnumerable<StudentViewModel>>(listByLastName);
            return mappedByName;
        }

        public async Task<StudentViewModel> GetStudentById(int studentId)
        {
            var student = await _studentAppService.GetStudentById(studentId);
            var mapped = _mapper.Map<StudentViewModel>(student);
            return mapped;
        }

        public async Task<IEnumerable<StudentViewModel>> GetStudentByGroup(int groupId)
        {
            var list = await _studentAppService.GetStudentByGroup(groupId);
            var mapped = _mapper.Map<IEnumerable<StudentViewModel>>(list);
            return mapped;
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroups()
        {
            var list = await _groupAppService.GetGroupList();
            var mapped = _mapper.Map<IEnumerable<GroupViewModel>>(list);
            return mapped;
        }

        public async Task<StudentViewModel> CreateStudent(StudentViewModel studentViewModel)
        {
            var mapped = _mapper.Map<StudentModel>(studentViewModel);
            if (mapped == null)
                throw new Exception($"Student could not be mapped.");

            var studentDto = await _studentAppService.Create(mapped);
            _logger.LogInformation($"Student successfully added - IndexPageService");

            var mappedViewModel = _mapper.Map<StudentViewModel>(studentDto);
            return mappedViewModel;
        }

        public async Task UpdateStudent(StudentViewModel studentViewModel)
        {
            var mapped = _mapper.Map<StudentModel>(studentViewModel);
            if (mapped == null)
                throw new Exception($"Student could not be mapped.");

            await _studentAppService.Update(mapped);
            _logger.LogInformation($"Student successfully added - IndexPageService");
        }

        public async Task DeleteStudent(StudentViewModel studentViewModel)
        {
            var mapped = _mapper.Map<StudentModel>(studentViewModel);
            if (mapped == null)
                throw new Exception($"Student could not be mapped.");

            await _studentAppService.Delete(mapped);
            _logger.LogInformation($"Student successfully added - IndexPageService");
        }
    }
}
