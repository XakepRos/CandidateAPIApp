using CandidateApp.Application.DTOs;
using CandidateApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandidateApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateCandidate([FromBody] CandidateDto candidateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _candidateService.AddOrUpdateCandidateAsync(candidateDto);
            return Ok();
        }
    }
}
