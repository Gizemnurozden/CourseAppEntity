using System.Reflection.Metadata;
using CourseAppEntity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseAppEntity.Controllers
{
    public class StudentController : Controller
    {
        private readonly CourseContext _context;

        public StudentController(CourseContext context){
            _context= context;

        }
        public async Task<IActionResult> Index(){ //kayıt olan öğrencileri listeledim.
            var students = await _context.Students.ToListAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student model) //öğrenci oluşturdum ve dataya kaydettim.
        {
            _context.Students.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
           
        }
        public async Task<IActionResult> Edit(int? id ) //güncelleme
        {
            if (id == null)
            {
                return NotFound();
                
            }
            var std = await _context
            .Students
            .Include(s => s.CourseRegistrations)
            .ThenInclude(s => s.Course)
            .FirstOrDefaultAsync(s => s.StudentId == id);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student model) //güncelleme işlemini dataya kaydetme ve sayfaya dönme
        {
            if (id != model.StudentId)
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
                    if (!_context.Students.Any(s =>s.StudentId == model.StudentId))
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
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student==null)
            {
                return NotFound();
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
    }
}