namespace P1_PokemonReviewApp.Models
{
    public class PokemonCategory
    {
        // Class for Pokemon and Category Many-To-Many relationship join table
        public int PokemonId { get; set; }
        public int CategoryId { get; set; }
        public Pokemon Pokemon { get; set; }
        public Category Category { get; set; }
    }
}
