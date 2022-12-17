using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly DataContext _context;
        public OwnerRepository(DataContext context)
        {
            this._context = context;
        }
        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.OrderBy(o => o.Id).ToList();
        }

        public Owner GetOwner(int id)
        {
            return _context.Owners.Where(o => o.Id == id).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(po => po.PokemonId == pokeId).Select(p => p.Owner).ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(po => po.OwnerId == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(c => c.Id == ownerId); // Any returns a bool
        }

        public bool CreateOwner(Owner owner) // Create a new data
        {
            _context.Add(owner);
            return Save();
        }

        public bool UpdateOwner(Owner owner) // Update an exisit data
        {
            _context.Update(owner);
            return Save();
        }

        public bool Save() // Save data changing
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; // if saved retun ture; else return false
        }
        
    }
}
