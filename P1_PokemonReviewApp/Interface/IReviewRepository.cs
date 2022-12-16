using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);
        ICollection<Reviewer> GetReviewerByReview(int reviewId);
        ICollection<Review> GetReviewerReviews(int reviewerId);
        bool ReviewExisit(int id);
    }
}
