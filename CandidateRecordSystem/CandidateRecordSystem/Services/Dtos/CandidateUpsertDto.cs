using System.ComponentModel.DataAnnotations;

namespace CandidateRecordSystem.Services.Dtos
{
    public class CandidateUpsertDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PreferredTimeToCall { get; set; }
        public string LinkedInProfileUrl { get; set; }
        public string GithubUrl { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
