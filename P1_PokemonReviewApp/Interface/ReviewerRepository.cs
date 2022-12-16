using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public class ReviewerRepository : IReviewerRepository
    {
        private DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            this._context = context;
        }

        public Reviewer GetReviewer(int id)
        {
            return _context.Reviewers.Where(rv => rv.Id == id).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.OrderBy(rv => rv.Id).ToList();
        }

        public bool ReviewerExisit(int id)
        {
            return _context.Reviewers.Any(rv => rv.Id == id);
        }
    }
}
