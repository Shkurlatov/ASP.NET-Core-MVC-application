using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Persistence.Data;

namespace School.Application.Repositories
{
    public class CuratorRepository
    {
        private readonly SchoolContext _dbContext;

        public CuratorRepository(SchoolContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
