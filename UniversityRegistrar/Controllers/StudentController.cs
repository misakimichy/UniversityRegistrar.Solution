using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using UniversityRegistrar.Models;


namespace UniversityRegistrar.Controllers
{
    public class StudentController : Controller
    {
        private readonly UniversityRegistrarContext _db;

        public StudentController(UniversityRegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Students.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Student theStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            return View(theStudent);
        }

        public ActionResult Edit(int id)
        {
            var thisStudent = _db.Students.FirstOrDefault(student => student.StudentId == id);
            ViewBag.CourseId = new SelectList(_db.Courses, "CourseId", "Name");
            return View(thisStudent);
        }

        [HttpPost]
        public ActionResult Edit(Student student, int CourseId)
        {
            if (CourseId != 0)
            {
                _db.CourseStudent.Add(new CourseStudent() { CourseId = CourseId, StudentId = student.StudentId });
            }
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}