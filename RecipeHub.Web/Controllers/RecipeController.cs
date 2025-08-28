using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Data.Models;
using RecipeHub.Web.ViewModels.Recipe;
using RecipeHub.Services.Data.Interfaces;
using System.Threading.Tasks;

namespace RecipeHub.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        
        public RecipeController(IRecipeService _recipeService)
        {
           recipeService = _recipeService;
        }
        public async Task<IActionResult> All(string searchText, string[]checkedCategories)
        {

            var categories = await recipeService.GetCategoryNamesAsync();


            var list = await recipeService.GetAllRecipesAsync(searchText,checkedCategories);
           

            var model = new RecipeAndCategoryModel()
            {
                Recipes = list,
                CategoryNames = categories
            };

            ViewData["SearchText"] = searchText;

            return View(model);
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
            
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var RecipeId = await recipeService.AddRecipeAsync(model);

            return RedirectToAction("AddIngredients", new {id=RecipeId});
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

            await recipeService.AddStepsAsync(GuidId, steps);

            return RedirectToAction("AddCategory", new { id = GuidId });
        }
        [HttpGet]
        public IActionResult AddIngredients()
        {
            var model = new IngridientViewModel();
            model.Units = recipeService.GetUnitTypes();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddIngredients(string id, List<IngridientViewModel> Ingredients)
        {
            Guid GuidId = Guid.Parse(id);

            await recipeService.AddIngredientsAsync(GuidId, Ingredients);  

            return RedirectToAction("AddSteps", new { id = GuidId });
        }
        public async Task<IActionResult>Details(string id)
        {
            Guid GuidId = Guid.Parse(id);

            var model=await recipeService.GetDetailsModelAsync(GuidId);

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddCategory()
        {
            var list =await recipeService.GetCategoryNamesAsync();


            return View(list);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory(string id,string[] checkedCategories)
        {
            Guid GuidId = Guid.Parse(id);

           await recipeService.AddCategoriesAsync(GuidId, checkedCategories);

            return RedirectToAction("All");
        }
    }
}
