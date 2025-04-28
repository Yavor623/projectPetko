using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Data;
using WebApplication1.Models.Resources;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class ResourceController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ResourceController(ApplicationDbContext db) => _db = db;
        public IActionResult Index()
        {
            var resources = _db.Resources.ToList();
            return View(resources);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resource = new Resource
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    Link = model.Link,
                    CategoryId = model.CategoryId
                };

                _db.Resources.Add(resource);
                await _db.SaveChangesAsync();
                ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", model.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name");
            var theResource = _db.Resources.Find(id);
            var resource = new EditResourceViewModel
            {
                Id = theResource.Id,
                Title = theResource.Title,
                Description = theResource.Description,
                Link = theResource.Link,
                CategoryId = theResource.CategoryId,
                Resource = theResource
            };
            return View(resource);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditResourceViewModel model)
        {
            if (ModelState.IsValid)
            {
				var resource = _db.Resources.Find(id);

				resource.Id = model.Id;
				resource.Title = model.Title;
				resource.Description = model.Description;
				resource.Link = model.Link;
				resource.CategoryId = model.CategoryId;
				_db.Resources.Update(resource);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name", model.CategoryId);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ViewBag.Category = _db.Categories.Where(a => a.Id == id);
            var theResource = _db.Resources.Find(id);
            var resource = new DeleteResourceViewModel
            {
                Id = theResource.Id,
                Title = theResource.Title,
                Description = theResource.Description,
                Link = theResource.Link,
                CategoryId = theResource.CategoryId
            };
            return View(resource);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = _db.Resources.Find(id);
            if (resource != null)
            {
                _db.Resources.Remove(resource);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
