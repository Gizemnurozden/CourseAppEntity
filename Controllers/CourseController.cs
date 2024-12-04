using CourseAppEntity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseAppEntity.Controllers
{
    public class CourseController:Controller
    {
        private readonly CourseContext _context;
        public CourseController(CourseContext context){
            _context= context;

        }

         public async Task<IActionResult> Index()
         {
            var courses = await _context.Courses.ToListAsync();
            return View(courses);
         }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Course model) //öğrenci oluşturdum ve dataya kaydettim.
        {
            _context.Courses.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }

        public async Task<IActionResult> Edit(int? id ) //güncelleme
        {
            if (id == null)
            {
                return NotFound();
                
            }
            var crs = await _context
            .Courses
            .Include(c => c.CourseRegistrations)
            .ThenInclude(c => c.Student)
            .FirstOrDefaultAsync(c => c.CourseId == id);
            if (crs == null)
            {
                return NotFound();
            }
            return View(crs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Course model) //güncelleme işlemini dataya kaydetme ve sayfaya dönme
        {
            if (id != model.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    if (!_context.Courses.Any(s =>s.CourseId == model.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                          throw;
                    }
                  
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course==null)
            {
                return NotFound();
            }
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}