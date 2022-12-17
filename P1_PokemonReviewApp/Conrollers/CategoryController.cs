using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P1_PokemonReviewApp.Dto;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;
using P1_PokemonReviewApp.Repository;

namespace P1_PokemonReviewApp.Conrollers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller // Inhereted from controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))] // To make API looks cleaner
        public IActionResult getCategories() // Returning a list from table
        {
            var categories = _categoryRepository.GetCategories(); // Get categories from Repository

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories);
        }

        [HttpGet("{catgoryId}")] // link
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)] // response error
        public IActionResult getCategory(int categoryId) // Returning spesific line of data
        {
            if (!_categoryRepository.CategoryExisit(categoryId))
                return NotFound();

            var category = _categoryRepository.GetCategory(categoryId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{catgoryId}")] // get
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

        [HttpPost] // post
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly created");
        }

        [HttpPut("{categoryId}")] // put
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int categoryId, [FromBody] CategoryDto categoryUpdate)
        {
            if(categoryUpdate == null)
                return BadRequest(ModelState);

            if(categoryId != categoryUpdate.Id)
            {
                ModelState.AddModelError("", "This categoryId dose not exist!");
                return BadRequest(ModelState);
            }

            if (!_categoryRepository.CategoryExisit(categoryId))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var categoryMap = _mapper.Map<Category>(categoryUpdate);

            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while updating");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfuly updated");
        }

    }
}
