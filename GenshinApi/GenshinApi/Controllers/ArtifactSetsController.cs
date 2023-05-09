using GenshinApi.Dtos.ArtifactSetDtos;
using GenshinApi.Services.ArtifactSetServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GenshinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtifactSetsController : Controller
    {
        private readonly IArtifactSetService _artifactSetService;
        private readonly ILogger<ArtifactSetsController> _logger;

        public ArtifactSetsController(IArtifactSetService artifactSetService, ILogger<ArtifactSetsController> logger)
        {
            _artifactSetService = artifactSetService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all Artifact Sets in ArtifactSet Table
        /// </summary>
        /// <returns>A List of all ArtifactSets</returns>
        /// <response code="201">Successfully displayed all Artifact Sets</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllArtifactSets")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllArtifactSets()
        {
            try
            {
                var artifactSets = await _artifactSetService.GetAllArtifactSets();

                if (artifactSets.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(artifactSets);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets an Artifact Set with matching name
        /// </summary>
        /// <param name="name">Name of the Artifact Set</param>
        /// <returns>Artifact Set matching the name</returns>
        /// <remarks>
        /// Sample request
        /// 
        ///     GET/api/ArtifactSets/Name/Lavawalker
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Artifact Set</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Name</response>
        /// <response code="500">Internal server error</response>        
        [HttpGet("/api/[controller]/Name/{name}", Name = "GetArtifactSetbyName")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArtifactSetByName(string name)
        {
            try
            {
                var artifactSet = await _artifactSetService.GetArtifactSet(name);

                if (artifactSet == null)
                {
                    return NotFound($"Artifact Set with the name {name} does not exist.");
                }

                return Ok(artifactSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Gets an Artifact Set with matching Id
        /// </summary>
        /// <param name="id">Id of the existing ArtifactSet</param>
        /// <returns>Artifact Set with matching Id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/ArtifactSets/Id/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Artifact Set</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response>   
        [HttpGet("/api/[controller]/Id/{id}", Name = "GetArtifactSetbyId")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetArtifactSetBySetId(int id)
        {
            try
            {
                var artifactSet = await _artifactSetService.GetArtifactSet(id);

                if (artifactSet == null)
                {
                    return NotFound($"Artifact Set with the set Id {id} does not exist.");
                }

                return Ok(artifactSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Create a new Artifact Set
        /// </summary>
        /// <param name="artifactSet">New Artifact Set</param>
        /// <returns>Newly Created Artifact Set</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST/api/ArtifactSets
        ///     {
        ///         "name": "God's Hand",
        ///         "description": "Most powerful Artifact"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created Artifact Set</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateArtifactSet([FromBody] ArtifactSetCreationDto artifactSet)
        {
            try
            {
                if (await _artifactSetService.GetArtifactSet(artifactSet.Name) != null)
                {
                    return StatusCode(400, $"Artifact already Exists");
                }

                var newArtifactSet = await _artifactSetService.CreateArtifactSet(artifactSet);

                return CreatedAtRoute("GetArtifactSetById", new { Id = newArtifactSet.Id }, newArtifactSet);

            } 
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Update an existing Artifact Set matching Id
        /// </summary>
        /// <param name="id">Id of the existing ArtifactSet</param>
        /// <param name="artifactSet">New Data to replace the old Artifact Set</param>
        /// <returns>The newly updated Artifact Set</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/api/ArtifactSets/1
        ///     {
        ///         "name": "Dragon's Artifact",
        ///         "description": "Created from the flesh of a Dragon"
        ///     }
        ///     
        /// </remarks>
        /// <response code="201">Successfully Updated Artifact Set</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Inputs</response>
        /// <response code="500">Internal server error</response> 
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateArtifactSet(int id, [FromBody] ArtifactSetUpdateDto artifactSet)
        {
            try
            {
                var artifact = await _artifactSetService.GetArtifactSet(id);
                if(artifact ==  null)
                {
                    return NotFound();
                }

                artifactSet.Name = (artifactSet.Name != null) ? artifactSet.Name : artifact.Name;
                artifactSet.Description = (artifactSet.Description != null) ? artifactSet.Description : artifact.Description;

                if (await _artifactSetService.UpdateArtifactSet(id, artifactSet))
                {

                    artifact = await _artifactSetService.GetArtifactSet(id);
                    return CreatedAtRoute("GetArtifactSetById", new { Id = id }, artifact);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Delete an existing Artifact Set matching Id
        /// </summary>
        /// <param name="id">Id of the ArtifactSet to be deleted</param>
        /// <returns>Message stating delete is successful or not</returns>
        /// <remarks>
        /// Sample request:
        ///     
        ///     DELETE/api/ArtifactSets/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully Deleted Artifact Set</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response> 
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ArtifactSetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteArtifactSet(int id)
        {
            try
            {
                var artifact = await _artifactSetService.GetArtifactSet(id);
                if (artifact == null)
                {
                    return NotFound($"Artifact Set with id {id} does not exist");
                }

                var flag = await _artifactSetService.DeleteArtifactSet(id);
                if (flag == true)
                {
                    return Ok($"Successfully Deleted Artifact Set with id {id}");
                }
                else
                {
                    return NotFound($"Artifact Set with id {id} is deleted unsucessfully");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
