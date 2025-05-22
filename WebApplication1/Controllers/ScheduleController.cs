using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Schedules;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public ScheduleController(ApplicationDbContext db, UserManager<ApplicationUser> userManager) 
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var programs = _db.Schedules.Include(a => a.Category).ToList();
            return View(programs);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create() 
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name");
            return View();
        } 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var schedule = new Schedule
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Summary = model.Summary,
                    CategoryId = model.CategoryId
                };
                if (model.ImageFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(ms);
                        schedule.Image = ms.ToArray();
                    }
                }

                _db.Schedules.Add(schedule);
                await _db.SaveChangesAsync();
                ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", model.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewData["CategoryId"] = new SelectList(_db.Categories, "Id", "Name");
            var theSchedule = _db.Schedules.FirstOrDefault(a => a.Id == id);
            var schedule = new EditScheduleViewModel
            {
                //Id = id,
                ByteImage = theSchedule.Image,
                Summary = theSchedule.Summary,
                Content = theSchedule.Content,
                Title = theSchedule.Title,
                CategoryId = theSchedule.CategoryId
            };
            return View(schedule);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
				var schedule = _db.Schedules.FirstOrDefault(a => a.Id == id);

                schedule.Title = model.Title;
                schedule.Content = model.Content;
                schedule.Summary = model.Summary;
                schedule.CategoryId = model.CategoryId;
                if (model.ImageFile != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        await model.ImageFile.CopyToAsync(ms);
                        schedule.Image = ms.ToArray();
                    }
                }
                else
                {
                    schedule.Image = model.ByteImage;
                }
                _db.Schedules.Update(schedule);
                await _db.SaveChangesAsync();
                ViewBag.CategoryId = new SelectList(_db.Categories, "Id", "Name", model.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var theSchedule = _db.Schedules.FirstOrDefault(a => a.Id == id);
            ViewBag.ChosenCategory = _db.Categories.Where(a => a.Id == theSchedule.CategoryId);
            var schedule = new DeleteScheduleViewModel
            {
                Id = theSchedule.Id,
                Image = theSchedule.Image,
                Summary = theSchedule.Summary,
                Content = theSchedule.Content,
                Title = theSchedule.Title,
                CategoryId = theSchedule.CategoryId
            };
            return View(schedule);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = _db.Schedules.FirstOrDefault(a => a.Id == id);
            if (program != null)
            {
                _db.Schedules.Remove(program);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        [Authorize]
        [HttpGet]
        public IActionResult Add(int id)
        {
            var theSchedule = _db.Schedules.FirstOrDefault(a => a.Id == id);
            ViewBag.ChosenCategory = _db.Categories.Where(a => a.Id == theSchedule.CategoryId);
            var schedule = new DetailsScheduleViewModel
            {
                Id = theSchedule.Id,
                Image = theSchedule.Image,
                Summary = theSchedule.Summary,
                Content = theSchedule.Content,
                Title = theSchedule.Title,
                CategoryId = theSchedule.CategoryId
            };
            return View(schedule);
        }
        [Authorize]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddConfirmed(int id)
        {
            var program = 
                from schedule in _db.ApplicationUserSchedules
                where schedule.ApplicationUserId == _userManager.GetUserId(User)
                select schedule.ScheduleId;
            if (!program.Contains(id))
            {
                var addedSchedule = new ApplicationUserSchedule
                {
                    ScheduleId = id,
                    ApplicationUserId = _userManager.GetUserId(User)
                };
                _db.ApplicationUserSchedules.Add(addedSchedule);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Add));
        }
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var currentSchedule = _db.Schedules.Find(id);
            ViewBag.Category = _db.Categories.FirstOrDefault(a => a.Id==currentSchedule.CategoryId);
            var schedule = new DetailsScheduleViewModel
            {
                Id = currentSchedule.Id,
                Image = currentSchedule.Image,
                Summary = currentSchedule.Summary,
                Content = currentSchedule.Content,
                Title = currentSchedule.Title,
                CategoryId = currentSchedule.CategoryId
            };
            return View(schedule);
        }
    }
}
