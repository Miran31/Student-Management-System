using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Data.Repository.IRepository;
using StudentManagementSystem.Utility;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_admin)]
    public class StudentclassController : Controller
    {
        private readonly IStudentclassRepository _studentclassReposiroty;
        public StudentclassController(IStudentclassRepository studentclassRepository)
        {
            _studentclassReposiroty = studentclassRepository;
        }
        public IActionResult Index() {
            List<StudentClass> studentClasses = _studentclassReposiroty.GetAll().ToList();
            return View(studentClasses);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(StudentClass studentClass)
        {
            if (studentClass == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                _studentclassReposiroty.Add(studentClass);
                _studentclassReposiroty.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            StudentClass studentClass = _studentclassReposiroty.Get(u => u.StudentClassId == id);
            return View(studentClass);
        }
        [HttpPost]
        public IActionResult Edit(StudentClass studentClass)
        {
            if (studentClass == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _studentclassReposiroty.Update(studentClass);
                _studentclassReposiroty.Save();
                return RedirectToAction("Index");
            }
            return View(studentClass);
        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            StudentClass studentClass = _studentclassReposiroty.Get(u => u.StudentClassId == id);
            _studentclassReposiroty.Remove(studentClass);
            _studentclassReposiroty.Save();
            return RedirectToAction("Index");
        }
    }
}
