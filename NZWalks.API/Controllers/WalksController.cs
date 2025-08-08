using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, ILogger<WalksController> logger)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalks([FromQuery] string? filterOn, [FromQuery] string? filterQuery, 
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending = true, 
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
                var walks = await walkRepository.GetAllWalksAsync(filterOn, filterQuery,
                sortBy, isAscending ?? true, pageNumber, pageSize);
                var walkDto = mapper.Map<List<WalkDto>>(walks);
                return Ok(walkDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
        {
            var walk = await walkRepository.GetWalkByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walk);

            return Ok(walkDto);

        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddNewWalk([FromBody] AddNewWalkDto addNewWalkDto)
        {
            var walk = mapper.Map<Walk>(addNewWalkDto);
            walk = await walkRepository.AddNewWalkAsync(walk);

            var walkDto = mapper.Map<WalkDto>(walk);
            return CreatedAtAction(nameof(GetWalkById), new { id = walkDto.Id }, walkDto);


        }
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        public async Task<IActionResult> updateWalk([FromRoute] Guid id, [FromBody] UpdateWalkDto updateWalkDto)
        {
            var walk = mapper.Map<Walk>(updateWalkDto);
            walk = await walkRepository.UpdateWalkAsync(id, walk);

            if (walk == null)
            {
                return NotFound();
            }
            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> deleteWalk([FromRoute] Guid id)
        {
            var walk = await walkRepository.DeleteWalkByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walk);
            return Ok(walkDto);
        }
    }
}
