using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class RecipeAndCategoryModel
    {
        public IEnumerable<AllRecipesViewModel> Recipes { get; set; } = new HashSet<AllRecipesViewModel>();

        public IEnumerable<string> CategoryNames { get; set; } = new HashSet<string>();
    }
}
