using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Data.Models;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class DetailsViewModel
    {
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public Guid Id { get; set; }

        public IList<string> Steps { get; set; }= new List<string>();

        public IList<Ingredient> Ingredients { get; set;} = new List<Ingredient>();
    }
}
