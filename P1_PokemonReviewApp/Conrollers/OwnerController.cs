using Microsoft.AspNetCore.Mvc;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;
using P1_PokemonReviewApp.Repository;

namespace P1_PokemonReviewApp.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        public OwnerController(IOwnerRepository ownerRepository)
        {
            this._ownerRepository = ownerRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))] // To make API looks cleaner
        public IActionResult getOwners() // Returning a list from table
        {
            var owners = _ownerRepository.GetOnwes();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }

        [HttpGet("{ownerId}")] // link
        [ProducesResponseType(200, Type = typeof(Owner))]
        [ProducesResponseType(400)]
        public IActionResult getOwner(int ownerId) // Returning spesific line of data
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var owner = _ownerRepository.GetOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owner);
        }

        [HttpGet("pokemon/{ownerId}")] // link
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByOwner(int ownerId)
        {
            if (!_ownerRepository.OwnerExists(ownerId))
                return NotFound();

            var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("owner/{pokiId}")] // link
        [ProducesResponseType(200, Type = typeof(IEnumerable<Owner>))]
        [ProducesResponseType(400)]
        public IActionResult GetOwnerOfAPokemon(int pokiId)
        {
            if (!_ownerRepository.OwnerExists(pokiId))
                return NotFound();

            var owners = _ownerRepository.GetOwnerOfAPokemon(pokiId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(owners);
        }
    }
}
