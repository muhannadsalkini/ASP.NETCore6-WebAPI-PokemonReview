﻿using P1_PokemonReviewApp.Data;
using P1_PokemonReviewApp.Interface;
using P1_PokemonReviewApp.Models;

namespace P1_PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository // "ctrl ." to see the error
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            this._context = context;
        }

        public bool CategoryExisit(int id) // Check if there are a category with this id number
        {
            return _context.Categories.Any(c => c.Id == id); // Any returns a bool
        }


        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(p => p.Id).ToList();
        }

        public Category GetCategory(int id) // Only return one
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            // Select Pokemons with category Id as a list
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }
        
    }
}