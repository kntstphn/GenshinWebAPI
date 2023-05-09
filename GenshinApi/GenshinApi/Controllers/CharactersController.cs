using GenshinApi.Dtos.CharDto;
using GenshinApi.Services.CharacterServices;
using Microsoft.AspNetCore.Mvc;


namespace GenshinApi.Controllers
{
    [Route("api/Characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _charService;
        private readonly ILogger<CharactersController> _logger;


        public CharactersController(ICharacterService charService, ILogger<CharactersController> logger
            )
        {
            _charService = charService;
            _logger = logger;
        }

        /// <summary>
        /// Create a new Character
        /// </summary>
        /// <param name="charDto">New Character</param>
        /// <returns>Newly Created Character</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///       "name": "Diluc Ragnvindr",
        ///       "rarity": "5*",
        ///       "gender": "Male",
        ///       "weaponId": 15,
        ///       "regionId": 1,
        ///       "setId": 12,
        ///       "elemId": 1
        ///
        /// </remarks>
        /// <response code="201">Successfully created Character</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [HttpPost] //POST/api/Characters
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateChar([FromBody] CharacterCreationDto charDto)
        {
            try
            {
                if(!await _charService.GetNameExists(charDto.Name))
                {
                    var character = await _charService.CreateChar(charDto);
                    return CreatedAtRoute("GetCharById", new { Id = character.Id }, character);
                }
                return StatusCode(409, $"Client request conflict.");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets a Character with matching Id
        /// </summary>
        /// <param name="id">Id of the existing Character</param>
        /// <returns>Character with matching Id</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/Characters/Id/1
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed Character</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Id</response>
        /// <response code="500">Internal server error</response>  
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}", Name = "GetCharById")]//GET/api/Characters/{id}
        public async Task<IActionResult> GetCharById(int id)
        {
            try
            {
                var character = await _charService.GetCharById(id);
                if (character == null)
                {
                    _logger.LogInformation("No characters found.");
                    return NotFound($"Character with id {id} does not exist");
                }
                return Ok(character);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

        /// <summary>
        /// Gets all Characters or gets all characters by element name
        /// </summary>
        /// <param name="elementName">Elemental property of the character</param>
        /// <returns>All characters or all characters with the same element name</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET/api/Characters/Pyro
        /// 
        /// </remarks>
        /// <response code="201">Successfully displayed all Characters or all Characters by their element</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Client Request Conflict</response>
        /// <response code="500">Internal server error</response>   
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet(Name = "GetAllCharacters")]  //GET/api/Characters
        public async Task<IActionResult> GetAllCharacters([FromQuery] string? elementName = null)
        {
            try
            {
                var chars = elementName != null
                    ? await _charService.GetAllCharByElementName(elementName)
                    : await _charService.GetAllChar();

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
        /// Delete an existing Character matching Id
        /// </summary>
        /// <param name="id">Id of character to be deleted</param>
        /// <returns>A message on whether or not the character was successfully deleted</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     DELETE/api/Characters/1
        ///
        /// </remarks>
        /// <response code="201">Successfully removed the Character</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{id}", Name = "DeleteCharById")]
        public async Task<IActionResult> DeleteCharById(int id)
        {
            try
            {
                
                var character = await _charService.GetCharById(id);
                if (character == null)
                {
                    _logger.LogInformation("No characters found.");
                    return NotFound($"Character with id {id} does not exist");
                }
                var delete = await _charService.DeleteChar(id);
                return StatusCode(202, $"Char {id} is successfully deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
        /// <summary>
        /// Update an existing Character matching Id
        /// </summary>
        /// <param name="id">Id of the existing Character</param>
        /// <param name="character">New Data to replace the old Character</param>
        /// <returns>The newly updated Character</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PUT/api/Characters/1
        ///           {
        ///             "weaponId": 16,
        ///              "setId": 9
        ///           }
        ///    
        /// </remarks>
        /// <response code="201">Successfully Updated Character</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Invalid Inputs</response>
        /// <response code="500">Internal server error</response> 
    [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CharacterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}", Name = "UpdateCharById")] //PUT/api/Characters/{id}
        public async Task<IActionResult> UpdateCharById(int id, [FromBody] CharUpdateDto character)
        {
            try
            {
                //Check id if exists
                var checkChar = await _charService.GetCharById(id);
                if (checkChar == null)
                {
                    _logger.LogInformation("No charcater found.");
                    return NotFound($"Character with id {id} does not exist");
                }
                var update = await _charService.UpdateChar(id, character);
                return Ok($"Char with id {id} is successfully updated");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }

    }
}
