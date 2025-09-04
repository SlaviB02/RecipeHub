using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Data.Models;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class AllRecipesViewModel
    {   
        public string Name { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public Guid Id { get; set; }

        public ICollection<string> Categories { get; set; } = new HashSet<string>();
    }
}
