using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Conrollers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;
        public PokemonController(IPokemonRepository pokemonRepository)
        {
            this._pokemonRepository = pokemonRepository;
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

    }
}
