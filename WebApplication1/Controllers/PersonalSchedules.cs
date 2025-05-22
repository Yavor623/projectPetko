using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Manage.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.PersonalSchedule;
using WebApplication1.Models.Resources;

namespace WebApplication1.Controllers
{
    public class PersonalSchedules : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        public PersonalSchedules(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Index()
        {
            var userSchedules = _db.ApplicationUserSchedules.Include(a => a.Schedule.Category).Include(a => a.ApplicationUser).Where(a => a.ApplicationUserId == _userManager.GetUserId(User)).ToList();
                return View(userSchedules);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {

            var thePersonalSchedule = _db.Schedules.Include(a => a.Category).FirstOrDefault(a => a.Id == id);
            var personalSchedule = new DeletePersonScheduleViewModel
            {
				Id = id,
				Image = thePersonalSchedule.Image,
				Summary = thePersonalSchedule.Summary,
				Content = thePersonalSchedule.Content,
				Title = thePersonalSchedule.Title,
				CategoryId = thePersonalSchedule.CategoryId
			};
            return View(personalSchedule);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = _db.ApplicationUserSchedules.FirstOrDefault(a => a.Id == id);
            if (schedule != null)
            {
                _db.ApplicationUserSchedules.Remove(schedule);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
