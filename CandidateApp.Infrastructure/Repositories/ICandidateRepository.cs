using CandidateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CandidateApp.Infrastructure.Repositories
{
    public interface ICandidateRepository
    {
        Task AddOrUpdateAsync(Candidate candidate);
        Task<Candidate> GetByEmailAsync(string email);
    }
}
