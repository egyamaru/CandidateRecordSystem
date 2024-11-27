using CandidateRecordSystem.Core.Entities;

namespace CandidateRecordSystem.Services
{
    public interface ICandidateService
    {
        Task<Candidate> InsertOrUpdateCandidate(Candidate candidate);
    }
}