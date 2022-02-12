using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using School.Application.Mapper;
using School.Application.Models.Users;
using School.Domain.Entities.Users;
using School.Domain.Interfaces.Users;

namespace School.Application.Services.Users
{
    public class CuratorService : IUserService<CuratorModel>
    {
        private readonly IUserRepository<Curator> _curatorRepository;

        public CuratorService(IUserRepository<Curator> curatorRepository)
        {
            _curatorRepository = curatorRepository ?? throw new ArgumentNullException(nameof(curatorRepository));
        }

        public async Task<IEnumerable<CuratorModel>> GetAll()
        {
            var curatorList = await _curatorRepository.GetAllAsync();
            var mapped = ObjectMapper.Mapper.Map<IEnumerable<CuratorModel>>(curatorList);
            return mapped;
        }

        public async Task<CuratorModel> GetById(string curatorId)
        {
            var curator = await _curatorRepository.GetByIdAsync(curatorId);
            var mapped = ObjectMapper.Mapper.Map<CuratorModel>(curator);
            return mapped;
        }

        public async Task Update(CuratorModel curatorModel)
        {
            var editCurator = await _curatorRepository.GetByIdAsync(curatorModel.Id);
            if (editCurator == null)
                throw new ApplicationException($"Curator could not be loaded.");

            editCurator.FirstName = curatorModel.FirstName;
            editCurator.LastName = curatorModel.LastName;
            editCurator.GroupId = curatorModel.GroupId;

            await _curatorRepository.UpdateAsync(editCurator);
        }

        public async Task Delete(CuratorModel curatorModel)
        {
            var deletedCurator = await _curatorRepository.GetByIdAsync(curatorModel.Id);
            if (deletedCurator == null)
                throw new ApplicationException($"Curator could not be loaded.");

            await _curatorRepository.DeleteAsync(deletedCurator);
        }
    }
}
