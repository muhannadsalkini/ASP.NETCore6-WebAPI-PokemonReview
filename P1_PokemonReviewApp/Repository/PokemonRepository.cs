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

        public bool CreatePokemon(int ownerId, int categoryId, Pokemon pokemon) // Create a new data
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner() // Inster into PokemonOwner tabel
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Add(pokemonOwner); // Add pokemonOwner to the Add query

            var pokemonCategory = new PokemonCategory() // Inser into PokemonCategory table
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Add(pokemonCategory); // Add pokemonCategory to the Add query

            _context.Add(pokemon); // Add pokemon to the Add query

            return Save();
        }

        public bool UpdatePokemon(int ownerId, int categoryId, Pokemon pokemon) // Update an exisit data
        {
            var pokemonOwnerEntity = _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault();
            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var pokemonOwner = new PokemonOwner() // Inster into PokemonOwner tabel
            {
                Owner = pokemonOwnerEntity,
                Pokemon = pokemon,
            };

            _context.Update(pokemonOwner); // Add pokemonOwner to the Update query

            var pokemonCategory = new PokemonCategory() // Inser into PokemonCategory table
            {
                Category = category,
                Pokemon = pokemon,
            };

            _context.Update(pokemonCategory); // Add pokemonCategory to the Update query

            _context.Update(pokemon); // Add pokemon to the Update query

            return Save();
        }

        public bool Save() // Save data changing
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // if saved retun ture; else return false
        }
    }
}
