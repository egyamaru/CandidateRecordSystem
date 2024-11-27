using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Core.Interfaces;

namespace CandidateRecordSystem.Services
{
    public class CandidateService: ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<Candidate> InsertOrUpdateCandidate(Candidate candidate)
        {
            return await _candidateRepository.InsertOrUpdate(candidate);
        }
    }
}
