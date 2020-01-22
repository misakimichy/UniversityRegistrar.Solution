using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UniversityRegistrar.Models;


namespace UniversityRegistrar.Controllers
{
    public class CourseController : Controller
    {
        private readonly UniversityRegistrarContext _db;

        public CourseController(UniversityRegistrarContext db)
        {
            _db = db;
        }

        public ActionResult Index()
        {
            return View(_db.Courses.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Course theCourse)
        {
            _db.Courses.Add(theCourse);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            Course theCourse = _db.Courses.FirstOrDefault(course => course.CourseId == id);
            return View(theCourse);
        }
    }
}