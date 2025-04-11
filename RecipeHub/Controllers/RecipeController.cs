using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Data.Models;
using RecipeHub.Web.ViewModels.Recipe;

namespace RecipeHub.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRepository<Recipe> RecipeRepository;
        private readonly IRepository<Ingredient> IngredientRepository;
        public RecipeController(IRepository<Recipe> _RecipeRepository, IRepository<Ingredient> _IngredientRepository)
        {
            RecipeRepository=_RecipeRepository;
            IngredientRepository = _IngredientRepository;
        }
        public async Task<IActionResult> All()
        {
            var list = await RecipeRepository.GetAllAttached()
                .Where(r=>r.isDeleted == false)
                .Select(r =>new AllRecipesViewModel()
                {
                    Name = r.Name,
                    ImageUrl = r.ImageUrl,
                    Id = r.Id,
                })
                .ToListAsync();
                
            
            return View(list);
        }
        [HttpGet]
        public IActionResult Add()
        {
            AddRecipeModel model = new AddRecipeModel();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult>Add(AddRecipeModel model)
        {

            Guid Recipeid = Guid.NewGuid();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Recipe recipe = new Recipe()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
                Id = Recipeid
            };

            await RecipeRepository.AddAsync(recipe);

            return RedirectToAction("AddIngredients", new {id=Recipeid});
        }
        [HttpGet]
        public IActionResult AddSteps()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSteps(string id,List<string> steps)
        {
            Guid GuidId=Guid.Parse(id);

            var recipe = await RecipeRepository.GetByIdAsync(GuidId);

            foreach(var step in steps)
            {
                recipe.Steps.Add(step);
            }

            await RecipeRepository.UpdateAsync(recipe);
       
            return RedirectToAction("All");
        }
        [HttpGet]
        public IActionResult AddIngredients()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddIngredients(string id, List<string> ingredients)
        {
            Guid GuidId = Guid.Parse(id);

           

            foreach (var ingredient in ingredients)
            {
                string[]temp=ingredient.Split(" - ");
                Ingredient ingrd = new Ingredient()
                {
                    RecipeId = GuidId,
                    Name = temp[0],
                    Weight = temp[1]
                };
               await IngredientRepository.AddAsync(ingrd);
            }

           

            return RedirectToAction("AddSteps", new { id = GuidId });
        }
        public async Task<IActionResult>Details(string id)
        {
            Guid GuidId = Guid.Parse(id);
            var recipe = await RecipeRepository.GetAllAttached()
                .Include(r => r.Ingredients)
                .FirstOrDefaultAsync(r => r.Id == GuidId);
            
            DetailsViewModel model = new DetailsViewModel()
            {
                Id = GuidId,
                Name = recipe!.Name,
                ImageUrl = recipe.ImageUrl,
                Ingredients = recipe.Ingredients,
                Steps = recipe.Steps,
            };

            return View(model);
        }
    }
}
