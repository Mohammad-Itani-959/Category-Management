using BulkyBookWeb.Model;
using BulkyBookWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryClient _categoryClient;

        public CategoryController(CategoryClient categoryClient)
        {
            _categoryClient = categoryClient;
        }





        [HttpGet]
        public IActionResult Index(int page = 1)
        {

            ViewBag.CurrentPage = page;
            return View();
        }

        /*[HttpGet("getcategories/{pageNumber}")]
        public async Task<IActionResult> GetCategories(int pageNumber)
        {
             
            var data = await _categoryServices.GetCategories(pageNumber);
            return Ok(data); 
        }
        //GET
        public IActionResult Create()
        {
            return View();

        }

        //ICategoryService 
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayedOrder.ToString())
            {
                ModelState.AddModelError("name", "The name and displayedorder cant be equal");
            }

            if (ModelState.IsValid)
            {
                _categoryServices.Add(category);
                 
                TempData["success"] = "The category have been created successfuly";
                return RedirectToAction("Index");
            }
            return View(category);

        }
        //or i can do if iam comming from url 
        //[HttpGet("Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var category = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            var category = _categoryServices.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if(category.Name == category.DisplayedOrder.ToString())
            {
                ModelState.AddModelError("name", "Name and displayedorder cant be equal");
            }
            if (ModelState.IsValid)
            {
                _categoryServices.Edit(category);
                 
                TempData["success"] = "The category have been updated successfuly";
                return RedirectToAction("Index");
            }
            return View();
        }
        //GET
        public IActionResult Delete(int id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var cat = _categoryServices.Find(id);

            if(cat == null)
            {
                return NotFound();
            }

            return View(cat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category category)
        {
            
            _categoryServices.Delete(category);

             
            
            TempData["success"] = "The category have been removed successfuly";
            return RedirectToAction("Index");
        }*/
        //GET
        [HttpGet("Create")]
       public async Task<IActionResult> Create()
       {
           return View();
       }

       //ICategoryService 
       //POST
       [HttpPost("Create")]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Create(Category category)
       {
            await _categoryClient.Create(category);
            return RedirectToAction("Index", new { page = 1 });

        }
       //or i can do if iam comming from url 
       public async Task<IActionResult> Edit(int id)
       {
           
            var cat = await _categoryClient.GetCategoryForEdit(id);

           return View(cat);
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Edit(Category category)
       {

            await _categoryClient.UpdateCategory(category);
            return RedirectToAction("Index", new { page = 1 });
        }
       //GET
       public async Task<IActionResult> Delete(int id)
       {

            var cat = await _categoryClient.GetCategoryForEdit(id);
            return View(cat);
       }

       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Delete(Category category)
       {

            await _categoryClient.DeleteCategory(category);
            return RedirectToAction("Index",new { page = 1});
       }
    }
}
