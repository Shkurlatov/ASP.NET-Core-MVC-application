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
    public class GroupPageService : IGroupPageService
    {
        private readonly IGroupService _groupAppService;
        private readonly ICourseService _courseAppService;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupPageService> _logger;

        public GroupPageService(IGroupService groupAppService, ICourseService courseAppService, IMapper mapper, ILogger<GroupPageService> logger)
        {
            _groupAppService = groupAppService ?? throw new ArgumentNullException(nameof(groupAppService));
            _courseAppService = courseAppService ?? throw new ArgumentNullException(nameof(courseAppService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroups(string name, int groupId)
        {
            if (groupId != 0)
            {
                var group = await _groupAppService.GetGroupWithStudents(groupId);
                var list = new List<GroupModel> { group };
                var mapped = _mapper.Map<IEnumerable<GroupViewModel>>(list);
                return mapped;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                var list = await _groupAppService.GetGroupList();
                var mapped = _mapper.Map<IEnumerable<GroupViewModel>>(list);
                return mapped;
            }

            var listByName = await _groupAppService.GetGroupByName(name);
            var mappedByName = _mapper.Map<IEnumerable<GroupViewModel>>(listByName);
            return mappedByName;
        }

        public async Task<GroupViewModel> GetGroupById(int groupId)
        {
            var group = await _groupAppService.GetGroupById(groupId);
            var mapped = _mapper.Map<GroupViewModel>(group);
            return mapped;
        }

        public async Task<IEnumerable<GroupViewModel>> GetGroupByCourse(int courseId)
        {
            var list = await _groupAppService.GetGroupByCourse(courseId);
            var mapped = _mapper.Map<IEnumerable<GroupViewModel>>(list);
            return mapped;
        }

        public async Task<IEnumerable<CourseViewModel>> GetCourses()
        {
            var list = await _courseAppService.GetCourseList();
            var mapped = _mapper.Map<IEnumerable<CourseViewModel>>(list);
            return mapped;
        }

        public async Task<GroupViewModel> CreateGroup(GroupViewModel groupViewModel)
        {
            var mapped = _mapper.Map<GroupModel>(groupViewModel);
            if (mapped == null)
                throw new Exception($"Group could not be mapped.");

            var groupDto = await _groupAppService.Create(mapped);
            _logger.LogInformation($"Group successfully added - IndexPageService");

            var mappedViewModel = _mapper.Map<GroupViewModel>(groupDto);
            return mappedViewModel;
        }

        public async Task UpdateGroup(GroupViewModel groupViewModel)
        {
            var mapped = _mapper.Map<GroupModel>(groupViewModel);
            if (mapped == null)
                throw new Exception($"Group could not be mapped.");

            await _groupAppService.Update(mapped);
            _logger.LogInformation($"Group successfully added - IndexPageService");
        }

        public async Task DeleteGroup(GroupViewModel groupViewModel)
        {
            var mapped = _mapper.Map<GroupModel>(groupViewModel);
            if (mapped == null)
                throw new Exception($"Group could not be mapped.");

            await _groupAppService.Delete(mapped);
            _logger.LogInformation($"Group successfully added - IndexPageService");
        }
    }
}
