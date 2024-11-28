using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandidateRecordSystem.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _appDbContext;

        public CandidateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Candidate> InsertAsync(Candidate candidate)
        {

            _appDbContext.Add(candidate);
            await _appDbContext.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> UpdateAsync(Candidate candidate)
        {

            _appDbContext.Update(candidate);
            await _appDbContext.SaveChangesAsync();
            return candidate;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _appDbContext.Candidate.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }
    }
}
