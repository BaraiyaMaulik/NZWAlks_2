using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWAlks_2.API.Models.Domain;
using NZWAlks_2.API.Models.DTO;
using NZWAlks_2.API.Repository;

namespace NZWAlks_2.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //https://localhost:7259/Regions
    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //[HttpGet]
        //public IActionResult GetAllRegions()
        //{
        //    var regions = new List<Region>()
        //    {
        //        new Region
        //        {
        //            Id= Guid.NewGuid(),
        //            Name="Wellington",
        //            Code="WGL",
        //            Area=227755,
        //            Lat=-1.8822,
        //            Long=299.88,
        //            Population=500000
        //        },
        //        new Region
        //        {
        //            Id= Guid.NewGuid(),
        //            Name="Auckland",
        //            Code="AUCK",
        //            Area=227755,
        //            Lat=-1.8822,
        //            Long=299.88,
        //            Population=500000
        //        }
        //    };
        //    return Ok(regions);
        //}

        //[HttpGet]
        //public IActionResult GetAllRegions_Repo()
        //{
        //    var regions = regionRepository.GetAll();

        //    return Ok(regions);
        //}

        //[HttpGet]
        //public IActionResult GetAllRegions_Repo()
        //{
        //    var regions = regionRepository.GetAll();

        //    //return DTO regions
        //    var regionsDTO = new List<RegionDTO>(); 
        //    regions.ToList().ForEach(regionDomain => 
        //    {
        //        var regionDTO = new RegionDTO()
        //        {
        //            Id= regionDomain.Id,
        //            Name = regionDomain.Name,
        //            Code = regionDomain.Code,
        //            Area = regionDomain.Area,
        //            Lat = regionDomain.Lat,
        //            Long = regionDomain.Long,
        //            Population = regionDomain.Population  
        //        };  
        //        regionsDTO.Add(regionDTO);  
        //    });

        //    return Ok(regionsDTO);
        //}

        //[HttpGet]
        //public IActionResult GetAllRegions_Repo_AutoMapper()
        //{
        //    var regions = regionRepository.GetAll();

        //    //return DTO regions
        //    //var regionsDTO = new List<RegionDTO>();
        //    //regions.ToList().ForEach(regionDomain =>
        //    //{
        //    //    var regionDTO = new RegionDTO()
        //    //    {
        //    //        Id = regionDomain.Id,
        //    //        Name = regionDomain.Name,
        //    //        Code = regionDomain.Code,
        //    //        Area = regionDomain.Area,
        //    //        Lat = regionDomain.Lat,
        //    //        Long = regionDomain.Long,
        //    //        Population = regionDomain.Population
        //    //    };
        //    //    regionsDTO.Add(regionDTO);
        //    //});

        //    //Domain Model to DTO
        //    var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
        //    return Ok(regionsDTO);
        //}

        [HttpGet]
        public async Task<IActionResult> GetAllRegions_Repo_AutoMapper()
        {
            //Domain model
            var regions = await regionRepository.GetAll_async();

            //Domain Model to DTO
            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            //Domain Model
            var regionDomain = await regionRepository.GetAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Domain Model to DTO
            var responseRegionDTO = mapper.Map<RegionDTO>(regionDomain);
            return Ok(responseRegionDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AddRegionAsync(AddRegionRequest addRegionRequest)
        {
            //Validate the Request => if false
            if(!ValidateAddRegionAsync(addRegionRequest))
            {
                return BadRequest(ModelState);
            }


            //Request(DTO) to Domain Model
            var region = new Region()
            {
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,
            };

            //Pass Details to Repository
            region = await regionRepository.AddAsync(region);

            //Convert back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = region.Id,
                Code = addRegionRequest.Code,
                Name = addRegionRequest.Name,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population
            };

            //201 request to client(response)
            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            //Get region from database
            var regionDomain = await regionRepository.DeleteAsync(id);
            //If null not found
            if (regionDomain == null)
            {
                //404
                return NotFound();
            }

            // Convert back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                Area = regionDomain.Area,
                Lat = regionDomain.Lat,
                Long = regionDomain.Long,
                Population = regionDomain.Population
            };
            //return OK response to client
            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegionRequest updateRegionRequest)
        {
            //Validate the incoming Request => if false
            if(!ValidateUpdateRegionAsync(updateRegionRequest))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domain Model
            var regionDomain = new Region()
            {
                Code = updateRegionRequest.Code,
                Name = updateRegionRequest.Name,
                Area = updateRegionRequest.Area,
                Lat = updateRegionRequest.Lat,
                Long = updateRegionRequest.Long,
                Population = updateRegionRequest.Population
            };
            //Update Region using Repository
            regionDomain = await regionRepository.UpdateAsync(id, regionDomain);

            //If Null then Not Found
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Convert Domain Back to DTO
            var regionDTO = new RegionDTO()
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                Area = regionDomain.Area,
                Lat = regionDomain.Lat,
                Long = regionDomain.Long,
                Population = regionDomain.Population
            };

            //Return OK Response
            return Ok(regionDTO);
        }

        #region  Private Methods
        private bool ValidateAddRegionAsync(AddRegionRequest addRegionRequest)
        {
            if(addRegionRequest == null)
            {
                ModelState.AddModelError(nameof(addRegionRequest), 
                    $"{nameof(addRegionRequest)} Add Region Data is required.");
                return false; 
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Code), 
                    $"{nameof(addRegionRequest.Code)} cannot be null or empty or white space");
            }

            if (string.IsNullOrWhiteSpace(addRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(addRegionRequest.Name), 
                    $"{nameof(addRegionRequest.Name)} cannot be null or empty or white space");
            }

            if (addRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Area), 
                    $"{nameof(addRegionRequest.Area)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Lat), 
                    $"{nameof(addRegionRequest.Lat)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Long), 
                    $"{nameof(addRegionRequest.Long)} cannot be less than or equal to zero");
            }

            if (addRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegionRequest.Population), 
                    $"{nameof(addRegionRequest.Population)} cannot be less than  zero");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private bool ValidateUpdateRegionAsync(UpdateRegionRequest updateRegionRequest)
        {
            if (updateRegionRequest == null)
            {
                ModelState.AddModelError(nameof(updateRegionRequest),
                    $"{nameof(updateRegionRequest)} Add Region Data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Code))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Code),
                    $"{nameof(updateRegionRequest.Code)} cannot be null or empty or white space");
            }

            if (string.IsNullOrWhiteSpace(updateRegionRequest.Name))
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Name),
                    $"{nameof(updateRegionRequest.Name)} cannot be null or empty or white space");
            }

            if (updateRegionRequest.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Area),
                    $"{nameof(updateRegionRequest.Area)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.Lat <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Lat),
                    $"{nameof(updateRegionRequest.Lat)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.Long <= 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Long),
                    $"{nameof(updateRegionRequest.Long)} cannot be less than or equal to zero");
            }

            if (updateRegionRequest.Population < 0)
            {
                ModelState.AddModelError(nameof(updateRegionRequest.Population),
                    $"{nameof(updateRegionRequest.Population)} cannot be less than  zero");
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
