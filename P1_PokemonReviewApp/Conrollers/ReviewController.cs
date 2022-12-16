using Microsoft.AspNetCore.Mvc;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;
using P1_PokemonReviewApp.Repository;

namespace P1_PokemonReviewApp.Conrollers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            this._reviewRepository = reviewRepository;
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
            //if (!_reviewRepository.ReviewExisit(reviewerId))
                //return NotFound();

            var reviews = _reviewRepository.GetReviewerReviews(reviewerId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(reviews);
        }

    }
}
