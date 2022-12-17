using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;

        public ReviewRepository(DataContext context)
        {
            this._context = context;
        }

        public ICollection<Reviewer> GetReviewerByReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).Select(rv => rv.Reviewer).ToList();
        }

        public ICollection<Review> GetReviewerReviews(int reviewerId)
        {
            return _context.Reviews.Where(r => r.Reviewer.Id == reviewerId).ToList();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.Id).ToList();
        }

        public Review GetReview(int id)
        {
            return _context.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public bool ReviewExisit(int id)
        {
            return _context.Reviews.Any(r => r.Id == id);
        }

        public bool CreateReview(Review review) // Create a new data
        {
            _context.Add(review);
            return Save();
        }

        public bool Save() // Save data changing
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // if saved retun ture; else return false
        }
    }
}
