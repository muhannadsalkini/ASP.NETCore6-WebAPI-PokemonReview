using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;
using P1_PokemonReviewApp.Repository;
using PokemonReviewApp.Dto;

namespace P1_PokemonReviewApp.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewerRepository _reviewerRepository;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewRepository reviewRepository, IReviewerRepository reviewerRepository, IPokemonRepository pokemonRepository, IMapper mapper)
        {
            this._reviewRepository = reviewRepository;
            this._reviewerRepository = reviewerRepository;
            this._pokemonRepository = pokemonRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Review>))] // To make API looks cleaner
        public IActionResult getReviews() // Returning a list from table
        {
            var reviews = _reviewRepository.GetReviews(); // Get categories from Repository

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpGet("{reviewId}")] // link
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult getReview(int reviewId) // Returning spesific line of data
        {
            if (!_reviewRepository.ReviewExisit(reviewId))
                return NotFound();

            var review = _reviewRepository.GetReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(review);
        }

        [HttpGet("reviewer/{reviewId}")] // link
        [ProducesResponseType(200, Type = typeof(Reviewer))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerByReview(int reviewId) // Returning spesific line of data
        {
            if (!_reviewRepository.ReviewExisit(reviewId))
                return NotFound();

            var reviewer = _reviewRepository.GetReviewerByReview(reviewId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviewer);
        }

        [HttpGet("review/{reviewerId}")] // link
        [ProducesResponseType(200, Type = typeof(Review))]
        [ProducesResponseType(400)]
        public IActionResult GetReviewerReviews(int reviewerId) // Returning spesific line of data
        {
            if (!_reviewerRepository.ReviewerExisit(reviewerId))
                return NotFound();

            var reviews = _reviewRepository.GetReviewerReviews(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReview([FromQuery] int reviewerId, [FromQuery] int pokeId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
                return BadRequest(ModelState);

            var reviews = _reviewRepository.GetReviews()
                .Where(c => c.Title.Trim().ToUpper() == reviewCreate.Title.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (reviews != null)
            {
                ModelState.AddModelError("", "Review already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var reviewMap = _mapper.Map<Review>(reviewCreate);

            reviewMap.Pokemon = _pokemonRepository.GetPokemon(pokeId);
            reviewMap.Reviewer = _reviewerRepository.GetReviewer(reviewerId);


            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

    }
}
