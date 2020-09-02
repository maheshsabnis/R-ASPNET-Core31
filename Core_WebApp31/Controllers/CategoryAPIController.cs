using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp31.Models;
using Core_WebApp31.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core_WebApp31.Controllers
{
    // Endpoint for API Controller, invoked post MapControllerRoute() for API Request
    [Route("api/[controller]")]
    // Used to accept the Complex JSON Data from HTTP request Body and Map with CLR object
    // uses the Message Formatter deault is JSON
   [ApiController]
    public class CategoryAPIController : ControllerBase
    {
       
        IRepository<Category, int> catRepo;

        public CategoryAPIController(IRepository<Category, int> catRepo)
        {
            this.catRepo = catRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cats = await catRepo.GetAsync();
            return Ok(cats);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cat = await catRepo.GetAsync(id);
            return Ok(cat);
        }

        [HttpPost]
        //public async Task<IActionResult> Post([FromBody]Category cat)
        // public async Task<IActionResult> Post(string CategoryId, string CategoryName, int BasePrice)
        //public async Task<IActionResult> Post([FromQuery] Category cat)
       // [HttpPost("{CategoryId}/{CategoryName}/{BasePrice}")]
        // public async Task<IActionResult> Post([FromRoute] Category cat)
        public async Task<IActionResult> Post(Category cat)
        {
            //var cat = new Category()
            //{
            //     CategoryId = CategoryId,
            //     CategoeyName = CategoryName,
            //     BasePrice = BasePrice
            //};
            if (ModelState.IsValid)
            {
                if (cat.BasePrice < 0) throw new Exception("Base Price cannot be -Ve");
                cat = await catRepo.CreateAsync(cat);
                return Ok(cat);
            }
            
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Category cat)
        {
            if (ModelState.IsValid)
            {
                cat = await catRepo.UpdateAsync(id, cat);
                return Ok(cat);
            }

            return BadRequest(ModelState);
        }
    }
}
