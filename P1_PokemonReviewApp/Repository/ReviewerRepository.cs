using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private DataContext _context;

        public ReviewerRepository(DataContext context)
        {
            _context = context;
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

        public bool CreateReviewer(Reviewer reviewer) // Create a new data
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool Save() // Save data changing
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // if saved retun ture; else return false
        }
    }
}
