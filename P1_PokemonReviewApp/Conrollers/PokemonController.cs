using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using P1_PokemonReviewApp.Dto;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Conrollers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;
        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)
        {
            this._pokemonRepository = pokemonRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))] // To make API looks cleaner
        public IActionResult getPokemons() // Returning a list from table
        {
            var pokemons = _pokemonRepository.GetPokemons();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }

        [HttpGet("{pokeId}")] // link
        [ProducesResponseType(200, Type = typeof(Pokemon))]
        [ProducesResponseType(400)]
        public IActionResult getPokemon(int pokeId) // Returning spesific line of data
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var pokemon = _pokemonRepository.GetPokemon(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")] // link
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult getPokemonReviews (int pokeId) // Returning a spesific information with more flexibalty
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var pokemonReviews = _pokemonRepository.GetPokemonReviews(pokeId);

            if (!ModelState.IsValid) // If user passed wrong data type
                return BadRequest(ModelState);

            return Ok(pokemonReviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePokemon([FromQuery] int OwnerId, [FromQuery] int categoryId, [FromBody] PokemonDto pokemonCreate)
        {
            if (pokemonCreate == null)
                return BadRequest(ModelState);

            var pokemon = _pokemonRepository.GetPokemons()
                .Where(c => c.Name.Trim().ToUpper() == pokemonCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (pokemon != null)
            {
                ModelState.AddModelError("", "Pokemon already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pokemonMap = _mapper.Map<Pokemon>(pokemonCreate);

            if (!_pokemonRepository.CreatePokemon(OwnerId, categoryId, pokemonMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }

    }
}
