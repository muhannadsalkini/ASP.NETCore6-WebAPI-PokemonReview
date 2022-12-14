namespace P1_PokemonReviewApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Reviewer Reviewer { get; set; } // One Relationship
        public Pokemon Pokemon { get; set; } // One Relationship
    }
}
