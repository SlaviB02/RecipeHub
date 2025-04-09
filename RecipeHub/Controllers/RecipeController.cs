using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Data.Models;
using RecipeHub.Web.ViewModels.Recipe;

namespace RecipeHub.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRepository<Recipe> repository;
        public RecipeController(IRepository<Recipe> _repository)
        {
            repository=_repository;
        }
        public async Task<IActionResult> All()
        {
            var list = await repository.GetAllAttached()
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Recipe recipe = new Recipe()
            {
                Name = model.Name,
                ImageUrl = model.ImageUrl,
            };

            await repository.AddAsync(recipe);

            return RedirectToAction("All");
        }
        [HttpGet]
        public IActionResult AddSteps()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSteps(string id,string step)
        {
            Guid GuidId=Guid.Parse(id);

            var recipe=await repository.FirstOrDefaultAsync(x=>x.Id == GuidId);

            recipe.Steps.Add(step);

            await repository.UpdateAsync(recipe);

            return RedirectToAction("AddSteps");
        }
    }
}
