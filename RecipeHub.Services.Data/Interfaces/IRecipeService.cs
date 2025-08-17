using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeHub.Web.ViewModels.Recipe;

namespace RecipeHub.Services.Data.Interfaces
{
    public interface IRecipeService
    {
        Task<IEnumerable<AllRecipesViewModel>> GetAllRecipesAsync(string? searchText);

        Task<Guid>AddRecipeAsync(AddRecipeModel model);

        Task<bool>AddIngredientsAsync(Guid id,IEnumerable<string> ingredients);

        Task<bool>AddStepsAsync(Guid id,IEnumerable<string> steps);

        Task<DetailsViewModel> GetDetailsModelAsync(Guid id);

    }
}
