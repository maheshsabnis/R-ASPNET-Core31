using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp31.Models;
using Core_WebApp31.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core_WebApp31.Controllers
{
   
    public class CategoryController : Controller
    {
        IRepository<Category, int> catRepo;

        public CategoryController(IRepository<Category, int> catRepo)
        {
            this.catRepo = catRepo;
        }
        [Authorize(Policy = "readpolicy")]
        public async Task<IActionResult> Index()
        {
            var cats = await catRepo.GetAsync();
            return View(cats);
        }
        [Authorize(Policy = "writepolicy")]
        public  IActionResult Create()
        {
            var cat = new Category();
            return View(cat);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category cat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (cat.BasePrice < 0) throw new Exception("Base Price Cannot be  -ve");
                    cat = await catRepo.CreateAsync(cat);
                    return RedirectToAction("Index");
                }
                return View(cat);
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel() { 
                   ControllerName  = this.RouteData.Values["controller"].ToString(),
                   ActionName = this.RouteData.Values["action"].ToString(),
                   ErrroMessage = ex.Message
                
                });
            }
        }
    }
}
