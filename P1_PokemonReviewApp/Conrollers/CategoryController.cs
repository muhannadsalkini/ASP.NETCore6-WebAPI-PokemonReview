using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;
using P1_PokemonReviewApp.Repository;

namespace P1_PokemonReviewApp.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller // Inhereted from controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository) // 
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))] // To make API looks cleaner
        public IActionResult getCategories() // Returning a list from table
        {
            var categories = _categoryRepository.GetCategories(); // Get categories from Repository

            

            return Ok(categories);
        }

        [HttpGet("{catgoryId}")] // link
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]
        public IActionResult getCategory(int categoryId) // Returning spesific line of data
        {
            if (!_categoryRepository.CategoryExisit(categoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{catgoryId}")] // link
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            if (!_categoryRepository.CategoryExisit(categoryId))
                return NotFound();

            var pokemons = _categoryRepository.GetPokemonsByCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons);
        }
    }
}
