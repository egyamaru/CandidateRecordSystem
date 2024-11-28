using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Services.Dtos;

namespace CandidateRecordSystem.Services.Mappings
{
    public static class CandidateMappings
    {
        public static Candidate ToCandidate(this CandidateUpsertDto candidateUpsertDto) => new Candidate
        {
            FirstName = candidateUpsertDto.FirstName,
            LastName = candidateUpsertDto.LastName,
            GithubUrl = candidateUpsertDto.GithubUrl,
            LinkedInProfileUrl = candidateUpsertDto.LinkedInProfileUrl,
            Comments = candidateUpsertDto.Comments,
            PreferredTimeToCall = candidateUpsertDto.PreferredTimeToCall,
            PhoneNumber = candidateUpsertDto.PhoneNumber,
            Email = candidateUpsertDto.Email
        };

        public static CandidateUpsertResponseDto ToCandidateUpsertResponseDto(this Candidate candidate) => new CandidateUpsertResponseDto
        {
            FirstName = candidate.FirstName,
            LastName = candidate.LastName,
            GithubUrl = candidate.GithubUrl,
            LinkedInProfileUrl = candidate.LinkedInProfileUrl,
            Comments = candidate.Comments,
            PreferredTimeToCall = candidate.PreferredTimeToCall,
            PhoneNumber = candidate.PhoneNumber,
            CandidateId = candidate.CandidateId,
            Email = candidate.Email
        };
    }
}
