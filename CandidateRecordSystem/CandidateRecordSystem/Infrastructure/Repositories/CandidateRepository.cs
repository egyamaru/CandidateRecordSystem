using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CandidateRecordSystem.Infrastructure.Repositories
{
    public class CandidateRepository:ICandidateRepository
    {
        private readonly AppDbContext _appDbContext;

        public CandidateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Candidate> InsertOrUpdate(Candidate candidate)
        {

            var existingCandidate= await _appDbContext.Candidate.FirstOrDefaultAsync(c=>c.Email == candidate.Email);

            if( existingCandidate is null)
            {
                //insert
                await _appDbContext.AddAsync(candidate);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                //update
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.Email = candidate.Email;
                existingCandidate.Comments = candidate.Comments;
                existingCandidate.GithubUrl = candidate.GithubUrl;
                existingCandidate.LinkedInProfileUrl = candidate.LinkedInProfileUrl;

                await _appDbContext.SaveChangesAsync();
                candidate.CandidateId = existingCandidate.CandidateId;
            }
            
            return candidate;
        }
    }
}
