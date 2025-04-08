using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class AllRecipesViewModel
    {   
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public Guid Id { get; set; }
    }
}
