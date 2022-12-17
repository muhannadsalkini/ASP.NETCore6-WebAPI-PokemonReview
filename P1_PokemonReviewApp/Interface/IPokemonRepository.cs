using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons(); // ICollection return an only readable list
        Pokemon GetPokemon(int id); // Returns a detailed information defrent from ICollection
        Pokemon GetPokemon(String name);
        decimal GetPokemonReviews(int pokeId);
        bool PokemonExists(int pokeId);
        bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon); // Create data
        bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon); // Update data
        bool Save();  // Save data
    }
}
