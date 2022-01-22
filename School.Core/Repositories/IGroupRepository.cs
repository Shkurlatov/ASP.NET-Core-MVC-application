﻿using School.Core.Entities;
using School.Core.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace School.Core.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<IEnumerable<Group>> GetGroupListAsync();
        Task<IEnumerable<Group>> GetGroupByNameAsync(string name);
        Task<IEnumerable<Group>> GetGroupByCourseAsync(int courseId);
    }
}
