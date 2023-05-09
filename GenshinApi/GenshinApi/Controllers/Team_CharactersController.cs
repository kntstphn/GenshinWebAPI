using GenshinApi.Dtos.TeamCompositionDtos;
using GenshinApi.Services.Team_CharacterServices;
using Microsoft.AspNetCore.Mvc;

namespace GenshinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Team_CharactersController : Controller
    {
        private readonly ITeam_CharacterService _teamCharService;
        private readonly ILogger<Team_CharactersController> _logger;

        public Team_CharactersController(ITeam_CharacterService teamCharService, ILogger<Team_CharactersController> logger)
        {
                _teamCharService = teamCharService;
                _logger = logger;
        }

        /// <summary>
        /// Add Character to a Team
        /// </summary>
        /// <param name="characterId">Id of the character</param>
        /// <param name="teamId">Id of the team</param>
        /// <returns>Team Name and Character Name</returns>
        /// <remarks>
        /// 
        ///     POST/api/?Team_CharacterscharacterId=2andteamId=2
        /// 
        /// </remarks>
        /// <response code="201">Successfully added Character to Team</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTeam_Character(int characterId, int teamId)
        {
            try
            {
                    var newTeamChar = await _teamCharService.CreateTeamCharacter(characterId, teamId);

                    return Ok(newTeamChar);

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        /// <summary>
        /// GetTeam_Characters by CharacterId and Team Id
        /// </summary>
        /// <param name="characterId">Id of the Character</param>
        /// <param name="teamId">Id of the Team</param>
        /// <returns>Team_Characters object containing Data</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/?Team_CharacterscharacterId=2andteamId=2
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Team_Characters</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTeam_Character(int characterId, int teamId)
        {
            try
            {
                if (characterId == 0 && teamId > 0)
                {
                    var teamCharByTeamId = await _teamCharService.GetAllByTeamId(teamId);

                    return Ok(teamCharByTeamId);
                }
                else if (characterId > 0 && teamId == 0)
                {
                    var teamCharByCharId = await _teamCharService.GetAllByCharacterId(characterId);

                    return Ok(teamCharByCharId);
                }
                else if (characterId > 0 && teamId > 0)
                {
                    var teamChar = await _teamCharService.GetTeamChar(teamId, characterId);

                    return Ok(teamChar);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
            
        }

        /// <summary>
        /// Delete existing Character on a Team
        /// </summary>
        /// <param name="teamId">Id of the Team</param>
        /// <param name="characterId">Id of the Character</param>
        /// <returns>Message if successful or not</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE/api/?Team_CharactersteamId=2andcharacterId=2
        /// 
        /// </remarks>
        /// <response code="201">Successfully Deleted Character on Team</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TeamCompositionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTeam_Character(int teamId, int characterId)
        {
            try
            {
                var teams = await _teamCharService.GetTeamChar(teamId, characterId);
                if (teams == null)
                {
                    return NotFound($"Character in a Team with Team Id {teamId} and Character Id {characterId} does not exist");
                }

                var flag = await _teamCharService.DeleteTeamChar(teamId, characterId);

                _logger.LogInformation(flag.ToString());
                _logger.LogInformation(teamId.ToString());
                _logger.LogInformation(characterId.ToString());


                if (flag == true)
                {
                    return Ok($"Successfully Deleted Character Id {characterId}  at Team Id {teamId}");
                }
                else
                {
                    return NotFound($"Unsuccessful Deletion of Character Id {characterId}  at Team Id {teamId}");
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
