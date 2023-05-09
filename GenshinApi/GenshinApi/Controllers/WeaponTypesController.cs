using GenshinApi.Dtos.WeaponDto;
using GenshinApi.Models;
using GenshinApi.Services.WeaponServices;
using GenshinApi.Services.WeaponTypeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GenshinApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponTypesController : ControllerBase
    {
        private readonly ILogger<WeaponsController> _logger;
        private readonly IWeaponsService _weaponsService;
        private readonly IWeaponTypeService _wepTypeServ;
        private readonly ICreateWeaponUnderTypeService _createService;

        public WeaponTypesController(ILogger<WeaponsController> logger, IWeaponsService weaponsService, IWeaponTypeService wepTypeServ, ICreateWeaponUnderTypeService createService)
        {
            _logger = logger;
            _weaponsService = weaponsService;
            _wepTypeServ = wepTypeServ;
            _createService = createService;
        }

        //Get api/WeaponType/ GetAllWeaponType
        /// <summary>
        /// Gets all the weapon types
        /// </summary>
        /// <returns>An Enumerable of all weapon types</returns>
        /// <response code="200">Successfully displayed all weapon types</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllWeaponType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeaponType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWeaponType()
        {
            try
            {
                var wepType = await _wepTypeServ.GetAllWeaponType();
                if (wepType.IsNullOrEmpty())
                    return NoContent();
                return Ok(wepType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        //Get api/WeaponType/{id}/Weapons
        /// <summary>
        /// Gets all weapons by its specified weapon type id
        /// </summary>
        /// <param name="id">Id of the weapon type</param>
        /// <returns>Returns an enumerable of weapon/s in the specified weapon type id</returns>
        /// <response code="200">Successfully retrieved weapon/s by its weapon type id</response>
        /// <response code="404">Weapon type not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}/Weapons")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeaponType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWeaponsByWeaponId(int id)
        {
            try
            {
                var verify = await _wepTypeServ.GetId(id);
                if (!verify)
                {
                    _logger.LogInformation("No weapons found.");
                    return NotFound($"Weapon Type with id {id} does not exist");
                }
                var wepType = await _weaponsService.GetAllWeaponsByWeaponType(id);
                return Ok(wepType);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        /// <summary>
        /// Creates a weapon under a weapon type
        /// </summary>
        /// <param name="id">Weapon type id</param>
        /// <param name="newWeapon">Weapon details</param>
        /// <returns>Returns the new weapon created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/WeaponType/{id}/createWeaponUnderType
        ///     {
        ///         "name": "Sword of Ahm BuhZin",
        ///         "damage": 35,
        ///         "rarity": 4
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a weapon under a valid weapon type</response>
        /// <response code="400">Weapon details are invalid</response>
        /// <response code="409">Client request conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("{id}/WeaponUnderType", Name = "createWeaponUnderType")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeaponCreationUnderTypeDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWeaponUnderType(int id, [FromBody] WeaponCreationUnderTypeDto newWeapon)
        {
            try
            {
                // Check if weaponType exists
                var weapon = await _wepTypeServ.GetTypeById(id);
                if (weapon == null)
                    return NotFound($"Weapon type with id {id} does not exist.");

                var newWeaponId = await _createService.CreateWeaponUnderType(id, newWeapon);

                return CreatedAtAction(
                    nameof(WeaponsController.GetWeapon), // name of the action/function
                    "Weapons", // name of the controller
                    new { Id = newWeaponId.Id }, // parameter needed
                    $"Successfully created {newWeapon.Name} under type: {weapon.Type}"); // returned data

                //Task: add created at route
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, e.Message);
            }
        }
    }
}

