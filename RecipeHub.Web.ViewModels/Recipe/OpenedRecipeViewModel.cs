using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Data.Models;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class OpenedRecipeViewModel
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
      
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();


        public IList<string> Steps { get; set; } = new List<string>();
    }
}
