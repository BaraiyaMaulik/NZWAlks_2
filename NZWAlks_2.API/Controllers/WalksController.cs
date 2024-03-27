using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;
using NZWAlks_2.API.Repositories;

namespace NZWAlks_2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalksAsync()
        {
            //Fetch data from database - Domain Model
            var walkDomain = await walkRepository.GetAllAsync();
            //Convert domain walks to DTO Walks
            var walkDTO = mapper.Map<List<WalkDTO>>(walkDomain);

            //return response to client
            return Ok(walkDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkByIDAsync")]
        public async Task<IActionResult> GetWalkByIDAsync(Guid id)
        {
            //Fetch data from database - Domain Model
            var walkDomain = await walkRepository.GetAsync(id);
            //Convert domain walk to DTO Walk
            var walkDTO = mapper.Map<WalkDTO>(walkDomain);
            //return response to client
            return Ok(walkDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {
            //Convert DTO to domain object
            var domainWalk = new Walk()
            {
                Length = addWalkRequest.Length,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                WalkDifficultyId = addWalkRequest.WalkDifficultyId,

            };

            //Pass domain object to Repository to persist this 
            domainWalk = await walkRepository.AddAsync(domainWalk);

            //Convert domain object back to DTO
            var walkDTO = mapper.Map<WalkDTO>(domainWalk);

            //Send DTO response back to client
            return CreatedAtAction(nameof(GetWalkByIDAsync), new { id = walkDTO.Id }, walkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //Convert DTO object to Domain object
            var walkDomain = new Walk()
            {
                Length = updateWalkRequestDTO.Length,
                Name = updateWalkRequestDTO.Name,
                RegionId = updateWalkRequestDTO.RegionId,
                WalkDifficultyId = updateWalkRequestDTO.WalkDifficultyId
            };

            //Pass details to  Repository - Get Domain object in Response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            //Handle Null (Not Found)
            if (walkDomain == null)
            {
                return NotFound("No Data found for this ID");
            }

            //Convert Domain Model  to DTO Model
            var walkDTO = new WalkDTO()
            {
                Id = walkDomain.Id,
                Length = walkDomain.Length,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                WalkDifficultyId = walkDomain.WalkDifficultyId,
            };

            //Return Response to Client with Http Request
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Call Repository to Delete Walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound("There is no data for this ID");
            }
            var walkDTO = mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDTO);
        }
    }
}
