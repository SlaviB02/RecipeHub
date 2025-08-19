using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Models;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Services.Data.Interfaces;
using RecipeHub.Web.ViewModels.Recipe;
using Microsoft.AspNetCore.Hosting;


namespace RecipeHub.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> RecipeRepository;
        private readonly IRepository<Ingredient> IngredientRepository;
        private readonly IHostingEnvironment env;
        
        public RecipeService(IRepository<Recipe> _RecipeRepository, IRepository<Ingredient> _IngredientRepository,IHostingEnvironment _env)
        {
            env = _env;
            RecipeRepository = _RecipeRepository;
            IngredientRepository = _IngredientRepository;
        }

        public async Task<bool> AddIngredientsAsync(Guid id, IEnumerable<string> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                string[] temp = ingredient.Split(" - ");
                Ingredient ingrd = new Ingredient()
                {
                    RecipeId = id,
                    Name = temp[0],
                    Weight = temp[1]
                };
                await IngredientRepository.AddAsync(ingrd);
            }
            return true;
        }

        public async Task<Guid> AddRecipeAsync(AddRecipeModel model)
        {
            Guid Recipeid = Guid.NewGuid();
            string uniqueFileName = null;

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                
                string uploadsFolder = Path.Combine(env.WebRootPath, "images");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

               
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(stream);
                }
            }
            Recipe recipe = new Recipe()
            {
                Name = model.Title,
                ImageUrl = "/images/" + uniqueFileName,
                Id = Recipeid
            };

            await RecipeRepository.AddAsync(recipe);

            return Recipeid;
        }

        public async Task<bool> AddStepsAsync(Guid id, IEnumerable<string> steps)
        {
            var recipe = await RecipeRepository.GetByIdAsync(id);

            foreach (var step in steps)
            {
                recipe.Steps.Add(step);
            }

            await RecipeRepository.UpdateAsync(recipe);

            return true;
        }

        public async Task<IEnumerable<AllRecipesViewModel>> GetAllRecipesAsync(string? searchText)
        {
            var query = RecipeRepository.GetAllAttached()
               .Where(r => r.isDeleted == false)
               .Select(r => new AllRecipesViewModel()
               {
                   Name = r.Name,
                   ImageUrl = r.ImageUrl,
                   Id = r.Id,
               });

             if(!String.IsNullOrEmpty(searchText))
            {
                query = query.Where(r => r.Name.ToLower().Contains(searchText.ToLower()));
            }

            var list = await query.ToListAsync();
               

            return list;
        }

        public async Task<DetailsViewModel> GetDetailsModelAsync(Guid id)
        {
            var recipe = await RecipeRepository.GetAllAttached()
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == id);

            DetailsViewModel model = new DetailsViewModel()
            {
                Id = id,
                Name = recipe!.Name,
                ImageUrl = recipe.ImageUrl,
                Ingredients = recipe.Ingredients,
                Steps = recipe.Steps,
            };

            return model;
        }
    }
}
