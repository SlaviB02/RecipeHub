using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RecipeHub.Data.Models;
using static RecipeHub.Common.ValidationConstants;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class AddRecipeModel
    {
        [Required]
        [StringLength(RecipeNameMaxLength, MinimumLength = RecipeNameMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        public IFormFile ImageFile { get; set; } = null!;
      
    }
}
