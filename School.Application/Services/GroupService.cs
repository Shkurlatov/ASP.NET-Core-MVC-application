using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Application.Mapper;
using School.Application.Models;
using School.Application.Repositories;
using School.Domain.Entities;
using School.Domain.Interfaces;

namespace School.Application.Services
{
    public class GroupService : IService<GroupModel>
    {
        private readonly IRepository<Group> _groupRepository;

        public GroupService(IRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository ?? throw new ArgumentNullException(nameof(groupRepository));
        }

        public async Task<IEnumerable<GroupModel>> GetAll()
        {
            var groupList = await _groupRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task<GroupModel> GetById(int groupId)
        {
            var group = await _groupRepository.GetByIdAsync(groupId);
            var mapped = ObjectMapper.Mapper.Map<GroupModel>(group);
            return mapped;
        }

        public async Task<IEnumerable<GroupModel>> GetBySearch(string searchTerm)
        {
            var groupList = await _groupRepository.GetBySearchAsync(searchTerm);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task<IEnumerable<GroupModel>> GetByParent(int parentId)
        {
            var groupList = await _groupRepository.GetByParentAsync(parentId);
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<GroupModel>>(groupList);
            return mapped;
        }

        public async Task Create(GroupModel groupModel)
        {
            await ValidateGroupIfExist(groupModel);

            var mappedEntity = ObjectMapper.Mapper.Map<Group>(groupModel);
            if (mappedEntity == null)
                throw new ApplicationException($"Group could not be mapped.");

            await _groupRepository.AddAsync(mappedEntity);
        }

        public async Task Update(GroupModel groupModel)
        {
            var editGroup = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (editGroup == null)
                throw new ApplicationException($"Group could not be loaded.");

            editGroup.Name = groupModel.Name;

            await _groupRepository.UpdateAsync(editGroup);
        }

        public async Task Delete(GroupModel groupModel)
        {
            var deletedGroup = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (deletedGroup == null)
                throw new ApplicationException($"Group could not be loaded.");

            await _groupRepository.DeleteAsync(deletedGroup);
        }

        private async Task ValidateGroupIfExist(GroupModel groupModel)
        {
            var existingEntity = await _groupRepository.GetByIdAsync(groupModel.Id);
            if (existingEntity != null)
                throw new ApplicationException($"{groupModel.ToString()} with this id already exists");
        }
    }
}
