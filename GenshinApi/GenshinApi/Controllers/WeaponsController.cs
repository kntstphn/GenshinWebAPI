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
    public class WeaponsController : ControllerBase
    {
        private readonly ILogger<WeaponsController> _logger;
        private readonly IWeaponsService _weaponsService;
        private readonly IWeaponTypeService _wepTypeServ;
     
        public WeaponsController(ILogger<WeaponsController> logger, IWeaponsService weaponsService, IWeaponTypeService weaponTypeService)
        {
            _logger = logger;
            _weaponsService = weaponsService;
            _wepTypeServ = weaponTypeService;
        }

        //POST api/Weapons/
        /// <summary>
        /// Creates a weapon
        /// </summary>
        /// <param name="weapon">Weapon details</param>
        /// <returns>Returns the new weapon created</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/Weapons
        ///     {
        ///         "name": "Spear of Ahma baTwo Kahm",
        ///         "damage": 40,
        ///         "weaponType": 2,
        ///         "rarity": 5
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Successfully created a weapon</response>
        /// <response code="400">Weapon details are invalid</response>
        /// <response code="409">Client request conflict</response>
        /// <response code="500">Internal server error</response>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(WeaponCreationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateWeapon([FromBody] WeaponCreationDto weapon)
        {
            try
            {
                //If name exists, return 409: client request conflict (duplicate entry)
                if (!await _weaponsService.GetNameExists(weapon.Name))
                {
                    //If weaponType doesnt exists: no weapon types found
                    if (await _wepTypeServ.GetId(weapon.WeaponType))
                    {
                        var wep = await _weaponsService.CreateWeapon(weapon);
                        return CreatedAtRoute("GetWeaponById", new { Id = wep.Id }, weapon);
                    }
                    _logger.LogInformation("No weapon types found.");
                    return NotFound($"Weapon Type with id {weapon.WeaponType} does not exist");
                }
                return StatusCode(409, $"Client request conflict.");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        //GET api/Weapons
        /// <summary>
        /// Gets all the weapons
        /// </summary>
        /// <returns>An Enumerable of all weapons</returns>
        /// <response code="200">Successfully displayed all weapons</response>
        /// <response code="204">No Content</response>
        /// <response code="409">Bad Request</response>
        /// <response code="500">Internal server error</response>
        [HttpGet(Name = "GetAllWeapons")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Weapons), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWeapons()
        {
            try
            {
                var weps = await _weaponsService.GetAllWeapons();
                if (weps.IsNullOrEmpty())
                    return NoContent();
                return Ok(weps);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong");
            }
        }

        // GET api/Weapons/{id}
        /// <summary>
        /// Gets a weapon by its specified id
        /// </summary>
        /// <param name="id">Id of the weapon</param>
        /// <returns>Returns an existing weapon with a specified id</returns>
        /// <response code="200">Successfully retrieved a weapon by its id</response>
        /// <response code="404">Weapon not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("{id}", Name = "GetWeaponById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Weapons), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWeapon(int id)
        {
            try
            {
                var weapon = await _weaponsService.GetWeaponsById(id);
                if(weapon == null)
                {
                    _logger.LogInformation("No weapons found.");
                    return NotFound($"Weapon with id {id} does not exist");
                }
                return Ok(weapon);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        //GET api/Weapons/Name/{name}
        /// <summary>
        /// Gets a weapon by its name
        /// </summary>
        /// <param name="name">Name of the weapon</param>
        /// <returns>Returns an existing weapon with the specified name</returns>
        /// <response code="200">Successfully retrieved a weapon by its name</response>
        /// <response code="404">Weapon not found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("Name/{name}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Weapons), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWeaponByName(string name)
        {
            try
            {
                if (!await _weaponsService.GetNameExists(name))
                {
                    _logger.LogInformation("No weapons found.");
                    return NotFound($"Weapon with name: {name} does not exist");
                }
                var wep = await _weaponsService.GetWeaponByName(name);
                return Ok(wep);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        //DELETE api/Weapons/{id}
        /// <summary>
        /// Deletes a weapon by its specified id
        /// </summary>
        /// <param name="id">Id of the weapon</param>
        /// <returns>Status code 202, weapon successfully deleted</returns>
        /// <response code="202">Successfully deleted a weapon by its id</response>
        /// <response code="404">Weapon not found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("{id}", Name = "DeleteWeaponById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            try
            {
                //Check id if exists
                var weapon = await _weaponsService.GetWeaponsById(id);
                if(weapon == null)
                {
                    _logger.LogInformation("No weapons found.");
                    return NotFound($"Weapon with id {id} does not exist");
                }
                var delete = await _weaponsService.DeleteWeapon(id);
                return StatusCode(202, $"Weapon id {id} is successfully deleted");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }

        //UPDATE api/Weapons/
        /// <summary>
        /// Updates the details of a weapon by its specified id
        /// </summary>
        /// <param name="id">Id of the weapon</param>
        /// <param name="weapons">Details to be updated</param>
        /// <returns>Status code 200, weapon is successfully updated</returns>
        /// <response code="200">Successfully updated a weapon</response>
        /// <response code="400">Weapon details are invalid</response>
        /// <response code="409">Client request conflict</response>
        /// <response code="404">Weapon not found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("{id}", Name = "UpdateWeaponById")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateWeapon(int id, [FromBody] WeaponCreationDto weapons)
        {
            try
            {
                //Check if weapon id exists
                var weapon = await _weaponsService.GetWeaponsById(id);
                if (weapon != null)
                {
                    //Check if name exists
                    if (!await _weaponsService.GetNameExists(weapons.Name))
                    {
                        //Check if weapon type exists
                        if (await _wepTypeServ.GetId(weapons.WeaponType))
                        {
                            var update = await _weaponsService.UpdateWeapon(id, weapons);
                            return Ok($"Weapon id {id} is successfully updated");
                        }
                        _logger.LogInformation("No weapon types found.");
                        return NotFound($"Weapon Type with id {weapons.WeaponType} does not exist");
                    }
                    return StatusCode(409, $"Client request conflict.");
                }
                _logger.LogInformation("No weapons found.");
                return NotFound($"Weapon with id {id} does not exist");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, "Something went wrong.");
            }
        }
    }

}

