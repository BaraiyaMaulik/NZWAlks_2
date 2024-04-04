using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWAlks_2.API.Data;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;
using NZWAlks_2.API.Repositories;


namespace NZWAlks_2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalkDifficultiesController : Controller
    {
        private readonly IWalkDifficultiesRepository walkDifficultiesRepository;
        private readonly IMapper mapper;

        public WalkDifficultiesController(IWalkDifficultiesRepository walkDifficultiesRepository, IMapper mapper)
        {
            this.walkDifficultiesRepository = walkDifficultiesRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalkDifficulties()
        {
            var walkDifficultyDomain = await walkDifficultiesRepository.GetAllAsync();

            //Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<List<WalkDifficulty>>(walkDifficultyDomain);

            return Ok(walkDifficultyDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDifficultiesByID")]
        public async Task<IActionResult> GetWalkDifficultiesByID(Guid id)
        {
            var walkDifficultyDomain = await walkDifficultiesRepository.GetByIdAsync(id);
            if (walkDifficultyDomain == null)
            {
                return NotFound("No data found for this ID");
            }

            //Convert Domain object To DTO object
            var walkDifficultyDTO = mapper.Map<WalkDifficulty>(walkDifficultyDomain);

            //Return DTO object response to Client
            return Ok(walkDifficultyDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddWalkDifficultyAsync(AddWalkDifficultyDTO addWalkDifficultyDTO)
        {
            //Validate the incoming request => if false
            if (!ValidateAddWalkDifficultyAsync(addWalkDifficultyDTO))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO object to Domain object
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = addWalkDifficultyDTO.Code,
            };

            //Call Repository
            walkDifficultyDomain = await walkDifficultiesRepository.AddAsync(walkDifficultyDomain);

            //Convert Domain to DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficultyRequestDTO>(walkDifficultyDomain);

            //return response to client
            return CreatedAtAction(nameof(GetWalkDifficultiesByID), new { id = walkDifficultyDTO.Id }, walkDifficultyDTO);

        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDifficultyAsync(Guid id,  UpdateWalkDifficultyRequestDTO updateWalkDifficultyRequestDTO)
        {
            //Validate the incoming request => if false
            if (!ValidateUpdateWalkDifficultyAsync(updateWalkDifficultyRequestDTO))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domain Model
            var walkDifficultyDomain = new WalkDifficulty()
            {
                Code = updateWalkDifficultyRequestDTO.Code
            };

            //Call Domain Repository to Update
            walkDifficultyDomain = await walkDifficultiesRepository.UpdateAsync(id, walkDifficultyDomain);

            if (walkDifficultyDomain == null)
            {
                return NotFound("Data Not found");
            }

            //Convert Domain To DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficultyRequestDTO>(walkDifficultyDomain);

            //return response to client
            return Ok(walkDifficultyDTO);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkDifficultyAsync(Guid id)
        {
            //Call Domain Repository to delete the Data
            var walkDifficultyDomain =await  walkDifficultiesRepository.DeleteAsync(id);

            if (walkDifficultyDomain == null)
            {
                return NotFound("Data not found for this ID");
            }

            //Convert Domain Model to DTO
            var walkDifficultyDTO = mapper.Map<WalkDifficulty>(walkDifficultyDomain);
            
            return Ok(walkDifficultyDTO);
        }

        #region Private Methods

        private bool ValidateAddWalkDifficultyAsync(AddWalkDifficultyDTO addWalkDifficultyDTO)
        {
            if (addWalkDifficultyDTO == null)
            {
                ModelState.AddModelError(nameof(addWalkDifficultyDTO),
                    $"{nameof(addWalkDifficultyDTO)} cannot be empty or Required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addWalkDifficultyDTO.Code))
            {
                ModelState.AddModelError(nameof(addWalkDifficultyDTO.Code), $"{nameof(addWalkDifficultyDTO.Code)} is a required field");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateWalkDifficultyAsync(UpdateWalkDifficultyRequestDTO updateWalkDifficultyRequestDTO)
        {
            if (updateWalkDifficultyRequestDTO == null)
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequestDTO),
                    $"{nameof(updateWalkDifficultyRequestDTO)} cannot be empty");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateWalkDifficultyRequestDTO.Code))
            {
                ModelState.AddModelError(nameof(updateWalkDifficultyRequestDTO.Code),
                    $"{nameof(updateWalkDifficultyRequestDTO.Code)} is a required field");
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
