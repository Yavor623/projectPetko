using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.Schedules;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ScheduleController(ApplicationDbContext db) => _db = db;

        public IActionResult Index()
        {
            var programs = _db.Schedules.ToList();
            return View(programs);
        }
        [HttpGet]
        public IActionResult Create() => View();
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
                    ImageUrl = model.ImageUrl,
                    Summary = model.Summary
                };

                _db.Schedules.Add(schedule);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var theSchedule = _db.Schedules.Find(id);
            var schedule = new EditScheduleViewModel
            {
                Id = theSchedule.Id,
                ImageUrl = theSchedule.ImageUrl,
                Summary = theSchedule.Summary,
                Content = theSchedule.Content,
                Title = theSchedule.Title,
                Schedule = theSchedule
            };
            return View(schedule);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
				var schedule = _db.Schedules.Find(id);

                schedule.Id = model.Id;
                schedule.Title = model.Title;
                schedule.Content = model.Content;
                schedule.ImageUrl = model.ImageUrl;
                schedule.Summary = model.Summary;
                _db.Schedules.Update(schedule);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var theSchedule = _db.Schedules.Find(id);
            var schedule = new DeleteScheduleViewModel
            {
                Id = theSchedule.Id,
                ImageUrl = theSchedule.ImageUrl,
                Summary = theSchedule.Summary,
                Content = theSchedule.Content,
                Title = theSchedule.Title,
            };
            return View(schedule);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = _db.Schedules.Find(id);
            if (program != null)
            {
                _db.Schedules.Remove(program);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
