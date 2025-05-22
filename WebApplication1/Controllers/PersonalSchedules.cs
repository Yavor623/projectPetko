using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

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
        public IActionResult Index()
        {
            var userSchedules = 
                from schedule in _db.ApplicationUserSchedules.Include(a => a.Schedule).Include(a => a.ApplicationUser)
                where schedule.ApplicationUserId == _userManager.GetUserId(User)
                select schedule;
                return View(userSchedules);
        }
        
    }
}
