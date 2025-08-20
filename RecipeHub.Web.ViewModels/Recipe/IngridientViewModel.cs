using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Data.Models.Enums;

namespace RecipeHub.Web.ViewModels.Recipe
{
    public class IngridientViewModel
    {
        public string Unit { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Quantity { get; set; } = null!;

        public IEnumerable<string>? Units { get; set; }
        
    }
}
