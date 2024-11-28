using CandidateRecordSystem.Core.Entities;

namespace CandidateRecordSystem.Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetByEmailAsync(string email);
        Task<Candidate> InsertAsync(Candidate candidate);
        Task<Candidate> UpdateAsync(Candidate candidate);
    }
}
