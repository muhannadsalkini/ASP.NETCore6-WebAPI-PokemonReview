using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Interface
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners(); // ICollection return an only readable list
        Owner GetOwner(int id); // Returns a detailed information defrent from ICollection
        ICollection<Owner> GetOwnerOfAPokemon(int pokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        bool CreateOwner(Owner owner);
        bool UpdateOwner(Owner owner);
        bool Save();
    }
}
