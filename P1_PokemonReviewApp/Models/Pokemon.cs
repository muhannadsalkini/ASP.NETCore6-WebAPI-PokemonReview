namespace P1_PokemonReviewApp.Models
{
    public class Pokemon // table
    {
        public int Id { get; set; } // column
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; } // Many Relationship
        // Using ICollecton becouse Reviews is a list of objects

    }
}

// Write prop than double-tab Tab to creat a Property faster. You can switch using tab also.
