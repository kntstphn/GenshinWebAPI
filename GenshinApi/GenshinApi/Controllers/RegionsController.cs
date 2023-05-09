using Microsoft.AspNetCore.Mvc;
using GenshinApi.Dtos.RegionDto;
using GenshinApi.Services.CharacterServices;
using GenshinApi.Services.RegionServices;
using Microsoft.IdentityModel.Tokens;
using GenshinApi.Dtos.CharDto;

namespace GenshinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IRegionServices _regionServices;
        private readonly ILogger<RegionsController> _logger;
        public RegionsController(IRegionServices regionServices, ILogger<RegionsController> logger
            , ICharacterService characterService)
        {
            _regionServices = regionServices;
            _logger = logger;
            _characterService = characterService;
        }

        /// <summary>
        /// Creates a new Region
        /// </summary>
        /// <param name="regionDto">New Region</param>
        /// <returns>Newly Created Region</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST/api/Regions
        ///     {
        ///         "name": "Philip",
        ///         "regionInspiredFrom": "Philippines",
        ///         "regionDescription": "An Archipelago"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created Region</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost(Name = "Add Region")]
        public async Task<IActionResult> AddRegion([FromBody] RegionCreationDto regionDto)
        {
            try
            {
                var newRegion = await _regionServices.AddRegion(regionDto);
                return CreatedAtRoute("GetRegionById", new { id = newRegion.Id }, newRegion);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
           
        }

        /// <summary>
        /// Gets a Region by its Id
        /// </summary>
        /// <param name="id">Id of the Region to be taken</param>
        /// <returns>The Region with the matching Id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/Regions/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Region</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "GetRegionById")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            try
            {
                var region = await _regionServices.GetRegionById(id);
                if (region == null)
                {
                    _logger.LogInformation("Region must have been destroyed");
                    return NotFound($"Region with id {id} does not exist");
                }
                return Ok(region);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Yikes");
            }
        }

        /// <summary>
        /// Gets all Region
        /// </summary>
        /// <returns>The List of all Regions</returns>
        /// <response code="201">Successfully displayed all Regions</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetAllRegions")]
        public async Task<IActionResult> GetAllRegion()
        {
            try
            {
                var regions = await _regionServices.GetAllRegion();

                if (regions.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(regions);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Get all Characters or by region name
        /// </summary>
        /// <param name="regionName">Name of the region</param>
        /// <returns>Characters from the region based on the region name given</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/Regions/Name/Liyue
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed all Characters or all characters by RegionName</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("Name", Name = "GetCharByRegionName")]
        public async Task<IActionResult> GetAllCharacters([FromQuery] string? regionName = null)
        {
            try
            {
                var chars = regionName != null
                    ? await _regionServices.GetAllCharByRegionName(regionName)
                    : await _characterService.GetAllChar();

                if (!chars.Any())
                {
                    _logger.LogInformation("No characters found.");
                    return NoContent(); // Status Code 204
                }

                return Ok(chars); // Status Code 200
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }


        /// <summary>
        /// Deletes a Region with matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Message if deletion of the Region was a success or not </returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE/api/Regions/1
        ///
        ///</remarks>
        /// <response code="201">Successfully removed the Region</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}", Name = "DeleteRegionById")]
        public async Task<IActionResult> DeleteRegionById(int id)
        {
            try
            {
                var region = await _regionServices.GetRegionById(id);
                if (region == null)
                {
                    _logger.LogInformation("Does not exist");
                    return NotFound($"Character with id {id} does not exist");
                }
                var remove = await _regionServices.DeleteRegionById(id);
                return StatusCode(200, $"Successfully removed Region with id {id}");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Yikes");
            }
        }

        /// <summary>
        /// Updates a Region with the matching Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="region"></param>
        /// <returns> Message if the Region was updated successfully or not </returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/api/Regions/1
        ///     {
        ///         "name": "Philipss",
        ///         "regionInspiratedFrom": "Philippines",
        ///         "regionDescription": "Home of Pinoys"
        ///     }
        ///
        ///</remarks>
        /// <response code="201">Successfully updated the Region</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}", Name = "UpdateRegionSetById")]
        public async Task<IActionResult> UpdateRegionSetById(int id, [FromBody] RegionCreationDto region)
        {
            try
            {
                var check = await _regionServices.GetRegionById(id);
                if (check == null)
                {
                    _logger.LogInformation("No Region found.");
                    return NotFound($"Region with id {id} does not exist");
                }
                var update = await _regionServices.UpdateRegionSetById(id, region);
                return Ok($"Region with id {id} is successfully updated");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

    }
}
