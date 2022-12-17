using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        bool ReviewerExisit(int id);
        bool CreateReviewer(Reviewer reviewer);
        bool Save();
    }
}
