using CandidateApp.Application.DTOs;
using CandidateApp.Application.Services;
using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Tests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
        private readonly ICandidateService _candidateService;

        public CandidateServiceTests()
        {
            _candidateRepositoryMock = new Mock<ICandidateRepository>();
            _candidateService = new CandidateService(_candidateRepositoryMock.Object);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldAddNewCandidate_WhenCandidateDoesNotExist()
        {
            // Arrange
            var candidateDto = new CandidateDto
            {
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "john.doe@example.com",
                CallTimeInterval = "9AM-5PM",
                LinkedInUrl = "https://linkedin.com/in/johndoe",
                GitHubUrl = "https://github.com/johndoe",
                FreeTextComment = "Some comment"
            };

            _candidateRepositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDto.Email)).ReturnsAsync((Candidate)null);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            // Assert
            _candidateRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.Is<Candidate>(c =>
                c.FirstName == candidateDto.FirstName &&
                c.LastName == candidateDto.LastName &&
                c.PhoneNumber == candidateDto.PhoneNumber &&
                c.Email == candidateDto.Email &&
                c.CallTimeInterval == candidateDto.CallTimeInterval &&
                c.LinkedInUrl == candidateDto.LinkedInUrl &&
                c.GitHubUrl == candidateDto.GitHubUrl &&
                c.FreeTextComment == candidateDto.FreeTextComment
            )), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateCandidateAsync_ShouldUpdateExistingCandidate_WhenCandidateExists()
        {
            // Arrange
            var existingCandidate = new Candidate
            {
                Id = 1,
                FirstName = "Jane",
                LastName = "Doe",
                PhoneNumber = "0987654321",
                Email = "jane.doe@example.com",
                CallTimeInterval = "10AM-6PM",
                LinkedInUrl = "https://linkedin.com/in/janedoe",
                GitHubUrl = "https://github.com/janedoe",
                FreeTextComment = "Old comment"
            };

            var candidateDto = new CandidateDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                PhoneNumber = "1234567890",
                Email = "jane.doe@example.com",
                CallTimeInterval = "9AM-5PM",
                LinkedInUrl = "https://linkedin.com/in/janedoe",
                GitHubUrl = "https://github.com/janedoe",
                FreeTextComment = "Updated comment"
            };

            _candidateRepositoryMock.Setup(repo => repo.GetByEmailAsync(candidateDto.Email)).ReturnsAsync(existingCandidate);

            // Act
            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);

            // Assert
            _candidateRepositoryMock.Verify(repo => repo.AddOrUpdateAsync(It.Is<Candidate>(c =>
                c.Id == existingCandidate.Id &&
                c.FirstName == candidateDto.FirstName &&
                c.LastName == candidateDto.LastName &&
                c.PhoneNumber == candidateDto.PhoneNumber &&
                c.Email == candidateDto.Email &&
                c.CallTimeInterval == candidateDto.CallTimeInterval &&
                c.LinkedInUrl == candidateDto.LinkedInUrl &&
                c.GitHubUrl == candidateDto.GitHubUrl &&
                c.FreeTextComment == candidateDto.FreeTextComment
            )), Times.Once);
        }
    }
}
