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
    }
}
