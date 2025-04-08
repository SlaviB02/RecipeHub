using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static RecipeHub.Common.ValidationConstants;

namespace RecipeHub.Data.Models
{
    public class Recipe
    {
        [Key]
        [Comment("The unique identifier of the recipe")]
        public Guid Id { get; set; }=Guid.NewGuid();

        [Required]
        [StringLength(RecipeNameMaxLength)]
        [Comment("The name of the recipe")]
        public string Name { get; set; } = null!;

        [Required]
        [Comment("The image of the recipe")]
        public string ImageUrl { get; set; }=null!;

        [Required]
        [Comment("Ingredients for the recipe")]
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        [Required]
        [Comment("The steps for making the recipe")]
        public IList<string> Steps { get; set; }=new List<string>();
    }
}
