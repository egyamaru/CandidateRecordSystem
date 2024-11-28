using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Core.Interfaces;
using CandidateRecordSystem.Enums;
using CandidateRecordSystem.Services;
using CandidateRecordSystem.Services.Dtos;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateRecordSystem.Tests.Tests
{
    [TestClass]
    public class CandidateServiceTests
    {
        private Mock<ICandidateRepository> _mockCandidateRepository;
        private CandidateService _candidateService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCandidateRepository = new Mock<ICandidateRepository>();
            _candidateService= new CandidateService(_mockCandidateRepository.Object);
        }

        [TestMethod]
        public async Task InsertOrUpsertCandidate_Insert_WhenNotExists()
        {
            var candidateUpsertDto = new CandidateUpsertDto
            {
                FirstName = "Harry",
                LastName = "Garry",
                Email = "harrygarry@gmail.com",
                Comments = "A dotnet developer",
                GithubUrl = "https://github.com/garryharry",
                LinkedInProfileUrl = "https://linkedin.com/garryharry",
                PhoneNumber = "+9779812345678",
                PreferredTimeToCall = "09:30"
            };

            var candidate = new Candidate
            {
                CandidateId=1,
                FirstName = "Harry",
                LastName = "Garry",
                Email = "harrygarry@gmail.com",
                Comments = "A dotnet developer",
                GithubUrl = "https://github.com/garryharry",
                LinkedInProfileUrl = "https://linkedin.com/garryharry",
                PhoneNumber = "+9779812345678",
                PreferredTimeToCall = TimeOnly.FromDateTime(DateTime.Now)
            };

            _mockCandidateRepository.Setup(s => s.GetByEmailAsync(candidateUpsertDto.Email)).ReturnsAsync((Candidate)null);
            _mockCandidateRepository.Setup(s => s.InsertAsync(It.IsAny<Candidate>())).ReturnsAsync(candidate);

            (Operation operation, CandidateUpsertResponseDto candidateUpsertResponseDto) = await _candidateService.InsertOrUpdateCandidateAsync(candidateUpsertDto);

            Assert.AreEqual(Operation.Insert, operation, "Expected Insert operation but got different one");
            Assert.IsNotNull(candidateUpsertResponseDto,"Newly created user should not be null but got null");
            Assert.AreEqual(candidateUpsertDto.Email, candidateUpsertResponseDto.Email);
            Assert.AreEqual(candidateUpsertDto.FirstName, candidateUpsertResponseDto.FirstName);
            Assert.AreEqual(candidateUpsertDto.LastName, candidateUpsertResponseDto.LastName);

            _mockCandidateRepository.Verify(r=>r.GetByEmailAsync(candidateUpsertDto.Email), Times.Once());
            _mockCandidateRepository.Verify(repo => repo.InsertAsync(It.IsAny<Candidate>()), Times.Once);
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Never);
        }

        [TestMethod]
        public async Task InsertOrUpsertCandidate_Update_WhenExists()
        {
            var preferedCallingTime= TimeOnly.FromDateTime(DateTime.Now);
            var candidateUpsertDto = new CandidateUpsertDto
            {
                FirstName = "Harry",
                LastName = "Garry",
                Email = "harrygarry@gmail.com",
                Comments = "A dotnet developer",
                GithubUrl = "https://github.com/garryharry",
                LinkedInProfileUrl = "https://linkedin.com/garryharry",
                PhoneNumber = "+9779812345678",
                PreferredTimeToCall = preferedCallingTime.ToString(),
            };

            var candidate = new Candidate
            {
                CandidateId = 1,
                FirstName = "Harry",
                LastName = "Garry",
                Email = "harrygarry@gmail.com",
                Comments = "A dotnet developer",
                GithubUrl = "https://github.com/garryharry",
                LinkedInProfileUrl = "https://linkedin.com/garryharry",
                PhoneNumber = "+9779812345678",
                PreferredTimeToCall = preferedCallingTime
            };

            var updatedCandidate = new Candidate
            {
                CandidateId = 1,
                FirstName = "Harry Larry",
                LastName = "Garry",
                Email = "harrygarry@gmail.com",
                Comments = "A fullstack developer",
                GithubUrl = "https://github.com/garryharry",
                LinkedInProfileUrl = "https://linkedin.com/garryharry",
                PhoneNumber = "+9779812345678",
                PreferredTimeToCall = preferedCallingTime
            };

            _mockCandidateRepository.Setup(s => s.GetByEmailAsync(candidateUpsertDto.Email)).ReturnsAsync(candidate);
            _mockCandidateRepository.Setup(s => s.UpdateAsync(It.IsAny<Candidate>())).ReturnsAsync(updatedCandidate);

            (Operation operation, CandidateUpsertResponseDto candidateUpsertResponseDto) = await _candidateService.InsertOrUpdateCandidateAsync(candidateUpsertDto);

            Assert.AreEqual(Operation.Update, operation, "Expected Insert operation but got different one");
            Assert.IsNotNull(candidateUpsertResponseDto, "Newly created user should not be null but got null");
            Assert.AreEqual(updatedCandidate.Email, candidateUpsertResponseDto.Email);
            Assert.AreEqual(updatedCandidate.FirstName, candidateUpsertResponseDto.FirstName);
            Assert.AreEqual(updatedCandidate.Comments, candidateUpsertResponseDto.Comments);

            _mockCandidateRepository.Verify(r => r.GetByEmailAsync(candidateUpsertDto.Email), Times.Once());
            _mockCandidateRepository.Verify(repo => repo.InsertAsync(It.IsAny<Candidate>()), Times.Never);
            _mockCandidateRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
        }
    }
}
