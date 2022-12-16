using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Repository
{
    public class PokemonRepository : IPokemonRepository // Class Inhereted from IPokemonRepository Interface
    {
        private readonly DataContext _context; // Type of DataContext
        public PokemonRepository(DataContext context) // Type "ctor" and double tab to creat constructer faster
        {
            this._context = context; // Context is a varable have our tables
        }

        public Pokemon GetPokemon(int id) // Implemented from IPokemonRepository
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault(); //Return one data line
        }

        public Pokemon GetPokemon(string name) // Implemented from IPokemonRepository
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault(); 
        }

        public decimal GetPokemonReviews(int pokeId) // Implemented from IPokemonRepository
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId);

            if (review.Count() <= 0)
                return 0;

            return (decimal)review.Count();
        }

        public ICollection<Pokemon> GetPokemons() // ICollection can only be a readable list
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList(); // Return list of data
        }

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId);
        }
    }
}
