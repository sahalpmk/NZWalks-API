using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.Dto;
using NZWalks.API.Repositories;
using System.Threading.Tasks;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
      
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<IActionResult> GetAllRegions()
        {
            var regions = await regionRepository.GetAllRegionsAsync();
            var regionsDto = mapper.Map<List<RegionDto>>(regions);
            return Ok(regionsDto);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(region);

            return Ok(regionDto);

        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> AddNewRegion([FromBody] AddNewRegionDto addNewRegionDto)
        {
            var region = mapper.Map<Region>(addNewRegionDto);
            region = await regionRepository.AddNewRegionAsync(region);

            var regionDto = mapper.Map<RegionDto>(region);
            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);


        }
        [HttpPut]
        [ValidateModel]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> updateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var region = mapper.Map<Region>(updateRegionDto);
            region = await regionRepository.UpdateRegionAsync(id, region);

            if(region == null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto> (region);
            return Ok(regionDto);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> deleteRegion([FromRoute] Guid id)
        {
            var region = await  regionRepository.DeleteRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDto>(region);
            return Ok(regionDto);
        }
    }
}
