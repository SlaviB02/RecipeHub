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
using RecipeHub.Data.Models.Enums;


namespace RecipeHub.Services.Data
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> RecipeRepository;
        private readonly IRepository<Ingredient> IngredientRepository;
        private readonly IRepository<Category> CategoryRepository;
        private readonly IRepository<RecipeCategory> RecipeCategoryRepository;
        private readonly IHostingEnvironment env;
        
        public RecipeService(IRepository<Recipe> _RecipeRepository,
            IRepository<Ingredient> _IngredientRepository,IHostingEnvironment _env, IRepository<Category> _CategoryRepository
            ,IRepository<RecipeCategory> _RecipeCategoryRepository)
        {
            env = _env;
            RecipeRepository = _RecipeRepository;
            IngredientRepository = _IngredientRepository;
            CategoryRepository = _CategoryRepository;
            RecipeCategoryRepository = _RecipeCategoryRepository;
        }

        public async Task<bool> AddCategoriesAsync(Guid id, IEnumerable<string> categories)
        {
            var cats=await CategoryRepository.GetAllAsync();
            foreach(var category in categories)
            {
                var categoryId = cats.FirstOrDefault(c => c.Name == category);
                if (categoryId != null)
                {
                    RecipeCategory rc = new RecipeCategory()
                    {
                        CategoryId = categoryId.Id,
                        RecipeId = id,
                    };
                    await RecipeCategoryRepository.AddAsync(rc);
                }
            }
            return true;
        }

        public async Task<bool> AddIngredientsAsync(Guid id, IEnumerable<IngridientViewModel> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                
                Ingredient ingrd = new Ingredient()
                {
                    RecipeId = id,
                    Name = ingredient.Name,
                    Weight = ingredient.Quantity+" "+ingredient.Unit
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

        public async Task<IEnumerable<AllRecipesViewModel>> GetAllRecipesAsync(string? searchText, IEnumerable<string> categories)
        {
            var query = RecipeRepository.GetAllAttached()
               .Where(r => r.isDeleted == false)
               .Select(r => new AllRecipesViewModel()
               {
                   Name = r.Name,
                   ImageUrl = r.ImageUrl,
                   Id = r.Id,
                   Categories=r.RecipeCategories.Select(x=>x.Category.Name).ToList()
               });

             if(!String.IsNullOrEmpty(searchText))
            {
                query = query.Where(r => r.Name.ToLower().Contains(searchText.ToLower()));
            }
             if(categories.Count()!=0)
            {
                foreach(var cat in categories)
                {
                    query = query.Where(x => x.Categories.Contains(cat));
                }
            }

            var list = await query.ToListAsync();
               

            return list;
        }

        public async Task<IEnumerable<string>> GetCategoryNamesAsync()
        {
            var list = await CategoryRepository.GetAllAttached().Select(x => x.Name).ToListAsync();

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

        public IEnumerable<string> GetUnitTypes()
        {
            var list = Enum.GetValues(typeof(UnitTypes)).Cast<UnitTypes>();

            return list.Select(m => m.ToString());
        }
    }
}
