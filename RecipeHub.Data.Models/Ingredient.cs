using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using static RecipeHub.Common.ValidationConstants;

namespace RecipeHub.Data.Models
{
    public class Ingredient
    {
        [Key]
        [Comment("The unique identifier of the Ingredient")]
        public Guid Id { get; set; }= Guid.NewGuid();

        [Required]
        [StringLength(IngredientNameMaxLength)]
        [Comment("Name of the ingredient")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(IngredientWeightMaxLength)]
        [Comment("Weight of the ingredient")]
        public string Weight { get; set; } = null!;

        public Recipe recipe { get; set; } = null!;
        [Required]
        public Guid RecipeId { get; set; }
    }
}
