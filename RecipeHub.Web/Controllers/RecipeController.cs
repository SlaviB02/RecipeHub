﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeHub.Data.Repository.Interfaces;
using RecipeHub.Data.Models;
using RecipeHub.Web.ViewModels.Recipe;
using RecipeHub.Services.Data.Interfaces;

namespace RecipeHub.Web.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;
        
        public RecipeController(IRecipeService _recipeService)
        {
           recipeService = _recipeService;
        }
        public async Task<IActionResult> All()
        {
            var list = await recipeService.GetAllRecipesAsync();
                
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

            await recipeService.AddIngredientsAsync(GuidId, ingredients);  

            return RedirectToAction("AddSteps", new { id = GuidId });
        }
        public async Task<IActionResult>Details(string id)
        {
            Guid GuidId = Guid.Parse(id);

            var model=await recipeService.GetDetailsModelAsync(GuidId);

            return View(model);
        }
    }
}
