namespace P1_PokemonReviewApp.Models
{
    public class PokemonOwner
    {
        // Class for Pokemon and Owner Many-To-Many relationship join table
        public int PokemonId { get; set; } // PokemonId <-> Owner
        public int OwnerId { get; set; } // OwnerId <-> Pokemon
        public Pokemon Pokemon { get; set; } // Actual enttity
        public Owner Owner { get; set; }
    }
}
