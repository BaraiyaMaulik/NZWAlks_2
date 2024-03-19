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

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
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
            var regions =await regionRepository.GetAll_async();

            //Domain Model to DTO
            var regionsDTO = mapper.Map<List<RegionDTO>>(regions);
            return Ok(regionsDTO);
        }
    }
}
