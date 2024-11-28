using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Core.Interfaces;
using CandidateRecordSystem.Enums;
using CandidateRecordSystem.Services.Dtos;
using CandidateRecordSystem.Services.Mappings;

namespace CandidateRecordSystem.Services
{
    public class CandidateService: ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }
        public async Task<(Operation,CandidateUpsertResponseDto)> InsertOrUpdateCandidateAsync(CandidateUpsertDto candidateUpsertDto)
        {
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidateUpsertDto.Email);
            if (existingCandidate is null)
            {
                var newCandidateDto = await _candidateRepository.InsertAsync(candidateUpsertDto.ToCandidate());
                return (Operation.Insert, newCandidateDto.ToCandidateUpsertResponseDto());
            }
            else
            {
                var candidateToUpdate= candidateUpsertDto.ToCandidate();
                candidateToUpdate.CandidateId=existingCandidate.CandidateId;
                var updatedCandidateDto = await _candidateRepository.UpdateAsync(candidateToUpdate);
                return (Operation.Update, updatedCandidateDto.ToCandidateUpsertResponseDto());
            }
        }
    }
}
