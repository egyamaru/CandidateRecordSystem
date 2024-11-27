using CandidateRecordSystem.Core.Entities;

namespace CandidateRecordSystem.Core.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate> InsertOrUpdate(Candidate candidate);
    }
}
