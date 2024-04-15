using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;
using NZWAlks_2.API.Repositories;
using NZWAlks_2.API.Repository;

namespace NZWAlks_2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;
        private readonly IRegionRepository regionRepository;
        private readonly IWalkDifficultiesRepository walkDifficultiesRepository;

        public WalksController(IWalkRepository walkRepository, IMapper mapper, IRegionRepository regionRepository, IWalkDifficultiesRepository walkDifficultiesRepository)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
            this.regionRepository = regionRepository;
            this.walkDifficultiesRepository = walkDifficultiesRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "reader")]
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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync([FromBody] AddWalkRequest addWalkRequest)
        {


            //Validate the incoming Request => if false
            if (!(await ValidateAddWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] UpdateWalkRequestDTO updateWalkRequestDTO)
        {

            //Validate the incoming request => if false
            if (!(await ValidateUpdateWalkAsync(updateWalkRequestDTO)))
            {
                return BadRequest(ModelState);
            }

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
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            //Call Domain Repository to Delete Walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound("There is no data for this ID");
            }
            var walkDTO = mapper.Map<WalkDTO>(walkDomain);
            return Ok(walkDTO);
        }

        #region Private Methods

        private async Task<bool> ValidateAddWalkAsync(AddWalkRequest addWalkRequest)
        {
            //if (addWalkRequest == null)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest),
            //        $"{nameof(addWalkRequest)} cannot be empty");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(addWalkRequest.Name))
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Name),
            //        $"{nameof(addWalkRequest.Name)} is required");
            //}

            //if (addWalkRequest.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(addWalkRequest.Length),
            //        $"{nameof(addWalkRequest.Length)} should be greater than Zero");
            //}

            var region = await regionRepository.GetAsync(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId),
                   $"{nameof(addWalkRequest.RegionId)} is Invalid");
            }

            var walkDifficulty = await walkDifficultiesRepository.GetByIdAsync(addWalkRequest.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDifficultyId),
                   $"{nameof(addWalkRequest.WalkDifficultyId)} is Invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(UpdateWalkRequestDTO updateWalkRequestDTO)
        {
            //if (updateWalkRequestDTO == null)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequestDTO),
            //        $"{nameof(updateWalkRequestDTO)} cannot be empty");
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(updateWalkRequestDTO.Name))
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequestDTO.Name),
            //        $"{nameof(updateWalkRequestDTO.Name)} is required");
            //}

            //if (updateWalkRequestDTO.Length <= 0)
            //{
            //    ModelState.AddModelError(nameof(updateWalkRequestDTO.Length),
            //        $"{nameof(updateWalkRequestDTO.Length)} should be greater than Zero");
            //}

            var region = await regionRepository.GetAsync(updateWalkRequestDTO.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequestDTO.RegionId),
                   $"{nameof(updateWalkRequestDTO.RegionId)} is Invalid");
            }

            var walkDifficulty = await walkDifficultiesRepository.GetByIdAsync(updateWalkRequestDTO.WalkDifficultyId);
            if (walkDifficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequestDTO.WalkDifficultyId),
                   $"{nameof(updateWalkRequestDTO.WalkDifficultyId)} is Invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
