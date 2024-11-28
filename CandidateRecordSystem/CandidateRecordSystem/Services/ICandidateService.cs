using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Enums;
using CandidateRecordSystem.Services.Dtos;

namespace CandidateRecordSystem.Services
{
    public interface ICandidateService
    {
        Task<(Operation, CandidateUpsertResponseDto)> InsertOrUpdateCandidateAsync(CandidateUpsertDto candidateUpsertDto);
    }
}