using CourseAppEntity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseAppEntity.Controllers
{
    public class CourseRegistrationController: Controller
    {
        private readonly CourseContext _context;
       public CourseRegistrationController(CourseContext context)
       {
        _context = context;
       }

       public async Task<IActionResult> Index()
       {
            var courseRegistration = await _context.CourseRegistrations
            .Include(x =>x.Student)
            .Include(x =>x.Course)
            .ToListAsync();

          return View(courseRegistration);
       }
       public async Task<IActionResult> Create()
        {
            ViewBag.Student = new SelectList(await _context.Students.ToListAsync(),"StudentId","NameLastName");
            ViewBag.Course = new SelectList(await _context.Courses.ToListAsync(),"CourseId","Title");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseRegistration model)
        {
            model.CourseDate = DateTime.Now;
          _context.CourseRegistrations.Add(model);
          await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
    }
}