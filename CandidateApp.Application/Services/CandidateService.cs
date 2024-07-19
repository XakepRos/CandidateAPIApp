using CandidateApp.Application.DTOs;
using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task AddOrUpdateCandidateAsync(CandidateDto candidateDto)
        {
            var candidate = new Candidate
            {
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                PhoneNumber = candidateDto.PhoneNumber,
                Email = candidateDto.Email,
                CallTimeInterval = candidateDto.CallTimeInterval,
                LinkedInUrl = candidateDto.LinkedInUrl,
                GitHubUrl = candidateDto.GitHubUrl,
                FreeTextComment = candidateDto.FreeTextComment
            };

            await _candidateRepository.AddOrUpdateAsync(candidate);
        }
    }
}
