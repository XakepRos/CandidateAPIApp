using CandidateApp.Domain.Entities;
using CandidateApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly CandidateDbContext _context;

        public CandidateRepository(CandidateDbContext context)
        {
            _context = context;
        }

        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _context.Candidates.SingleOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddOrUpdateAsync(Candidate candidate)
        {
            var existingCandidate = await GetByEmailAsync(candidate.Email);
            if (existingCandidate != null)
            {
                existingCandidate.FirstName = candidate.FirstName;
                existingCandidate.LastName = candidate.LastName;
                existingCandidate.PhoneNumber = candidate.PhoneNumber;
                existingCandidate.CallTimeInterval = candidate.CallTimeInterval;
                existingCandidate.LinkedInUrl = candidate.LinkedInUrl;
                existingCandidate.GitHubUrl = candidate.GitHubUrl;
                existingCandidate.FreeTextComment = candidate.FreeTextComment;

                _context.Candidates.Update(existingCandidate);
            }
            else
            {
                await _context.Candidates.AddAsync(candidate);
            }
            await _context.SaveChangesAsync();
        }
    }
}
