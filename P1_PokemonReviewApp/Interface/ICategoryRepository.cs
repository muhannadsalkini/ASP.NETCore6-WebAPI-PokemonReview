using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Pokemon> GetPokemonsByCategory(int categoryId);
        bool CategoryExisit(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
