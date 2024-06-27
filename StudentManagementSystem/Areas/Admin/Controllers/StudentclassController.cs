using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Data;
using StudentManagementSystem.Data.Repository.IRepository;
using StudentManagementSystem.Migrations;
using StudentManagementSystem.Models;
using StudentManagementSystem.Utility;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_admin)]
    public class StudentclassController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IStudentclassRepository _studentclassReposiroty;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserReposiroty;
        public StudentclassController(IStudentclassRepository studentclassRepository, UserManager<ApplicationUser> userManager, IApplicationUserRepository applicationUserReposiroty, ApplicationDbContext applicationDbContext)
        {
            _studentclassReposiroty = studentclassRepository;
            _userManager = userManager;
            _applicationUserReposiroty = applicationUserReposiroty;
            _db = applicationDbContext;
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



        //studennt controller

        public IActionResult StudentIndex()
        {
            List<ApplicationUser> Students=_applicationUserReposiroty.GetAll().ToList();
            return View(Students);
        }
        public IActionResult StudentCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentCreate(ApplicationUser applicationuser)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email=applicationuser.Student_Email,
                    UserName= applicationuser.Student_Email,
                    Student_Name= applicationuser.Student_Name,
                    Student_Email= applicationuser.Student_Email,
                    Gender= applicationuser.Gender,
                    Students_Id= applicationuser.Students_Id,
                    FathersName= applicationuser.FathersName,
                    MothersName= applicationuser.MothersName,
                    Phone= applicationuser.Phone,
                    GuardianAddress= applicationuser.GuardianAddress,
                    LoginEmail= applicationuser.Student_Email,
                    LoginPassword= applicationuser.LoginPassword,
                };
                var result = _userManager.CreateAsync(user, applicationuser.LoginPassword).GetAwaiter().GetResult();
                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, SD.Role_student).GetAwaiter().GetResult();
                    return RedirectToAction("StudentIndex");
                }
            }
            return View(applicationuser);
        }
        public IActionResult StudentEdit(string? id)
        {
            ApplicationUser applicationUser = _applicationUserReposiroty.Get(u => u.Id == id);
            return View(applicationUser);
        }

        [HttpPost]
        public async Task<IActionResult> StudentEdit(ApplicationUser applicationUser)
        {
            if(applicationUser == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    var studentFromDb = _applicationUserReposiroty.Get(u=>u.Id== applicationUser.Id);
                    if(studentFromDb== null)
                    {
                        return NotFound();
                    }
                    studentFromDb.Email = applicationUser.Student_Email;
                    studentFromDb.UserName = applicationUser.Student_Email;
                    studentFromDb.Student_Name = applicationUser.Student_Name;
                    studentFromDb.Student_Email = applicationUser.Student_Email;
                    studentFromDb.Gender = applicationUser.Gender;
                    studentFromDb.Students_Id = applicationUser.Students_Id;
                    studentFromDb.FathersName = applicationUser.FathersName;
                    studentFromDb.MothersName = applicationUser.MothersName;
                    studentFromDb.Phone = applicationUser.Phone;
                    studentFromDb.GuardianAddress = applicationUser.GuardianAddress;
                    _applicationUserReposiroty.Update(studentFromDb);
                    _applicationUserReposiroty.SaveAsync();
                    return RedirectToAction("StudentIndex");
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!_applicationUserReposiroty.Exists(applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(applicationUser);
            
        }
        [HttpPost]
        public async Task<IActionResult> StudentDelete(string? id)
        {
            var studenttobeDeleted = _applicationUserReposiroty.Get(u => u.Id == id);
            if(studenttobeDeleted == null)
            {
                return NotFound();
            }
            _applicationUserReposiroty.Remove(studenttobeDeleted);
            _applicationUserReposiroty.SaveAsync();

            return RedirectToAction("StudentIndex");
        }
    }
}
