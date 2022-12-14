namespace P1_PokemonReviewApp.Models
{
    public class PokemonOwner
    {
        // Class for Pokemon and Owner Many-To-Many relationship join table
        public int PokemonId { get; set; } // Id number
        public int OwnerId { get; set; }
        public Pokemon Pokemon { get; set; } // Actual enttity
        public Owner Owner { get; set; }
    }
}
