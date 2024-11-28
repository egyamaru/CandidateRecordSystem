using CandidateRecordSystem.Core.Entities;
using CandidateRecordSystem.Enums;
using CandidateRecordSystem.Services;
using CandidateRecordSystem.Services.Dtos;
using CandidateRecordSystem.Services.Mappings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CandidateRecordSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly ILogger<CandidateController> _logger;

        public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger)
        {
            _candidateService = candidateService;
            _logger = logger;
        }

        /// <summary>
        /// Insert a new Candidate or update an existing one based on the email
        /// </summary>
        /// <param name="candidateDto"></param>
        /// <returns>A newly created candidate or updated candidate information</returns>
        /// <response code="200">If insert or update operation was sucessfull</response>
        /// <response code="500">If insert or update operation failed</response>
        /// <response code="400">For any validation errors</response>
        [HttpPut]
        [ProducesResponseType(typeof(CandidateUpsertResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(500)]
        [Produces("application/json")]
        public async Task<IActionResult> UpsertAsync([FromBody] CandidateUpsertDto candidateDto)
        {
            try
            {
                (Operation operation, CandidateUpsertResponseDto candidateUpsertResponseDto) upsertResult = await _candidateService.InsertOrUpdateCandidateAsync(candidateDto);
                if(upsertResult.operation==Operation.Insert)
                    return Created($"/candidate/{upsertResult.candidateUpsertResponseDto.CandidateId}", upsertResult.candidateUpsertResponseDto); //dummy url for now /candidate/{candidateId}
                else
                    return Ok(upsertResult.candidateUpsertResponseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Processing failed");
                return StatusCode(500, "Processing failed");
            }
        }
    }
}
