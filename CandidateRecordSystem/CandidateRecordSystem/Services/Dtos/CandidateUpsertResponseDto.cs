﻿using System.ComponentModel.DataAnnotations;

namespace CandidateRecordSystem.Services.Dtos
{
    public class CandidateUpsertResponseDto
    {
        public int CandidateId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PreferredTimeToCall { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string GithubUrl { get; set; }
        public string Comments { get; set; }
    }
}
