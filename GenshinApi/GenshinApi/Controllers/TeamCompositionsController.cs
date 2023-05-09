using GenshinApi.Dtos.TeamCompositionDtos;
using GenshinApi.Services.TeamCompositionServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GenshinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamCompositionsController : Controller
    {
        private readonly ITeamCompositionService _teamCompService;
        private readonly ILogger<TeamCompositionsController> _logger;

        public TeamCompositionsController(ITeamCompositionService teamCompService, ILogger<TeamCompositionsController> logger)
        {
            _teamCompService = teamCompService;
            _logger = logger;
        }

        /// <summary>
        /// Get all Team Compositions in TeamComposition Table
        /// </summary>
        /// <returns>List of all Team Compositions</returns>
        /// <response code="201">Successfully displayed all Team Compositions</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllTeamComp")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTeamCompositions()
        {
            try
            {
                var teamComp = await _teamCompService.GetAll();

                if (teamComp.IsNullOrEmpty())
                {
                    return NoContent();
                }

                return Ok(teamComp);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Get Team Composition with matching Id
        /// </summary>
        /// <param name="id">Id of the existing Team Composition</param>
        /// <returns>Team Composition with matching Id</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     GET/api/TeamComposition/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Team Composition</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response>  
        [HttpGet("{id}", Name = "GetTeamCompositionById")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeamCompositionById(int id)
        {
            try
            {
                var teamComp = await _teamCompService.GetTeamById(id);

                if (teamComp == null)
                {
                    return NotFound($"Team Composition with the set Id {id} does not exist.");
                }

                return Ok(teamComp);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Create new Team Composition
        /// </summary>
        /// <param name="teamComp">New Team Composition</param>
        /// <returns>Newly Created Team Composition</returns>
        /// <remarks>
        /// Sample Request:
        /// 
        ///     POST/api/TeamComposition
        ///     {
        ///         "name": "Dendro Reactions"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created Team Composition</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateTeamComposition([FromBody] TeamCompositionCreationDto teamComp)
        {
            try
            {
                if (await _teamCompService.GetName(teamComp.Name))
                {
                    return StatusCode(409, $"Client request conflict.");
                }

                var newTeamComposition = await _teamCompService.CreateTeamComposition(teamComp);

                return CreatedAtRoute("GetTeamCompositionById", new { Id = newTeamComposition.Id }, newTeamComposition);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// Update an existing Team Composition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="teamComposition"></param>
        /// <returns>The newly updated Team Composition</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/api/TeamComposition/1
        ///     {
        ///         "name": "Abyss First Team"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully Updated Team Composition</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Input</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTeamComposition(int id, [FromBody] TeamCompositionCreationDto teamComposition)
        {
            try
            {
                var teamComp = await _teamCompService.GetTeamById(id);
                if (teamComp == null)
                {
                    return NotFound();
                }

                teamComposition.Name = teamComp.Name != null ? teamComposition.Name : teamComp.Name;

                if (await _teamCompService.UpdateTeamComposition(id, teamComposition))
                {

                    teamComp = await _teamCompService.GetTeamById(id);
                    return CreatedAtRoute("GetTeamCompositionById", new { Id = id }, teamComp);
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
        /// Delete an existing Team Composition
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message stating delete is successful or not</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE/api/TeamComposition/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully Deleted Team Composition</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response> 
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeamComposition(int id)
        {
            try
            {
                var teamComp = await _teamCompService.GetTeamById(id);
                if (teamComp == null)
                {
                    return NotFound($"Team Composition with id {id} does not exist");
                }

                var flag = await _teamCompService.DeleteTeamComposition(id);
                if (flag == true)
                {
                    return Ok($"Successfully Deleted Team Composition with id {id}");
                }
                else
                {
                    return NotFound($"Team Composition with id {id} is deleted unsucessfully");
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
