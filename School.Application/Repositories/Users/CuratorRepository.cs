using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities.Users;
using School.Domain.Interfaces.Users;
using School.Persistence.Data;

namespace School.Application.Repositories.Users
{
    public class CuratorRepository : IUserRepository<Curator>
    {
        private readonly SchoolContext _dbContext;

        public CuratorRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<Curator>> GetAllAsync()
        {
            return await _dbContext.Set<Curator>().Include(x => x.Group).AsNoTracking().ToListAsync();
        }

        public async Task<Curator> GetByIdAsync(string curatorId)
        {
            return await _dbContext.Set<Curator>().Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == curatorId);
        }

        public async Task UpdateAsync(Curator curator)
        {
            _dbContext.Entry(curator).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Curator curator)
        {
            _dbContext.Set<Curator>().Remove(curator);
            await _dbContext.SaveChangesAsync();
        }
    }
}
