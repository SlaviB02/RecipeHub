using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Models;

namespace RecipeHub.Data
{
    public class RecipeHubDbContext : IdentityDbContext
    {
        public RecipeHubDbContext(DbContextOptions<RecipeHubDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Recipe> Recipes { get; set; } = null!;

        public DbSet<Ingredient> Ingredients { get; set;} = null!;
    }
}
