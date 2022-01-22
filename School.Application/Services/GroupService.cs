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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppLogger<GroupService> _logger;

        public GroupService(IGroupRepository groupRepository, IAppLogger<GroupService> logger)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<GroupModel> GetGroupWithStudents(int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            var mapped = ObjectMapper.Mapper.Map<GroupModel>(group);
            return mapped;
        }

        public async Task<IEnumerable<GroupModel>> GetGroupList()
        {
            var groupList = await _groupRepository.GetGroupListAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task<GroupModel> GetGroupById(int groupId)
        {
            var group = await _groupRepository.GetGroupByIdAsync(groupId);
            var mapped = ObjectMapper.Mapper.Map<GroupModel>(group);
            return mapped;
        }

        public async Task<IEnumerable<GroupModel>> GetGroupByName(string name)
        {
            var groupList = await _groupRepository.GetGroupByNameAsync(name);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task<IEnumerable<GroupModel>> GetGroupByCourse(int courseId)
        {
            var groupList = await _groupRepository.GetGroupByCourseAsync(courseId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task<GroupModel> Create(GroupModel groupModel)
        {
            await ValidateGroupIfExist(groupModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Group>(groupModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Group could not be mapped.");

            var newEntity = await _groupRepository.AddAsync(mappedEntity);
            _logger.LogInformation($"Group successfully added - SchoolAppService");

            var newMappedEntity = ObjectMapper.Mapper.Map<GroupModel>(newEntity);
            return newMappedEntity;
        }

        public async Task Update(GroupModel groupModel)
        {
            ValidateGroupIfNotExist(groupModel);

            var editGroup = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (editGroup == null)
                throw new ApplicationException($"Group could not be loaded.");

            ObjectMapper.Mapper.Map<GroupModel, Group>(groupModel, editGroup);

            await _groupRepository.UpdateAsync(editGroup);
            _logger.LogInformation($"Group successfully updated - SchoolAppService");
        }

        public async Task Delete(GroupModel groupModel)
        {
            ValidateGroupIfNotExist(groupModel);
            var deletedGroup = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (deletedGroup == null)
                throw new ApplicationException($"Group could not be loaded.");

            await _groupRepository.DeleteAsync(deletedGroup);
            _logger.LogInformation($"Group successfully deleted - SchoolAppService");
        }

        private async Task ValidateGroupIfExist(GroupModel groupModel)
        {
            var existingEntity = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{groupModel.ToString()} with this id already exists");
        }

        private void ValidateGroupIfNotExist(GroupModel groupModel)
        {
            var existingEntity = _groupRepository.GetByIdAsync(groupModel.Id);
            if (existingEntity == null)
                throw new ApplicationException($"{groupModel.ToString()} with this id is not exists");
        }
    }
}
