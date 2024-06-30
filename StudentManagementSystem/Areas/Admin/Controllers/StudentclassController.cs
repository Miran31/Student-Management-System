using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Data;
using StudentManagementSystem.Data.Repository.IRepository;
using StudentManagementSystem.Migrations;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Viewmodel;
using StudentManagementSystem.Utility;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_admin)]
    public class StudentclassController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IStudentclassRepository _studentclassReposiroty;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUserRepository _applicationUserReposiroty;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StudentclassController(IStudentclassRepository studentclassRepository, UserManager<ApplicationUser> userManager, IApplicationUserRepository applicationUserReposiroty, ApplicationDbContext applicationDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _studentclassReposiroty = studentclassRepository;
            _userManager = userManager;
            _applicationUserReposiroty = applicationUserReposiroty;
            _db = applicationDbContext;
            _webHostEnvironment = webHostEnvironment;
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

        public async Task<IActionResult> StudentIndexAsync()
        {
            List<ApplicationUser> Students=_applicationUserReposiroty.GetAll(includeProterty:"StudentClass").ToList();
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<StudentClass> studentClasses = _studentclassReposiroty.GetAll().ToList();
            foreach(var i in Students)
            {
                //foreach(var classes in studentClasses)
                //{
                //    if(classes.StudentClassId == i.StudentClassId)
                //    {
                //        string classssssss = $"{classes.Year} - {classes.Semester}";
                //        //users.Add(classssssss);
                //        //Console.WriteLine(classssssss);
                        
                //    }
                //}
                var roles = await _userManager.GetRolesAsync(i);
                if (roles.Contains(SD.Role_student))
                {
                    users.Add(i);
                }
            }
            return View(users);
        }
        public IActionResult StudentCreate()
        {
            var studentClass = _studentclassReposiroty.GetAll().Select(u => new SelectListItem
            {
                Value = u.StudentClassId.ToString(), 
                Text = $"{u.Year} - {u.Semester}"
            }).ToList();
            ViewBag.Studentcls = studentClass;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> StudentCreate(ApplicationUser applicationuser,IFormFile ? file)
        {
            
            if (ModelState.IsValid)
            {
                string fileName="";
                var rootPath = _webHostEnvironment.WebRootPath;
                var studentPath = Path.Combine(rootPath + @"\Images\Student\");
                if (file != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    using (var fileStream = new FileStream(Path.Combine(studentPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                }
                var user = new ApplicationUser
                {
                    Email = applicationuser.Student_Email,
                    UserName = applicationuser.Student_Email,
                    Student_Name = applicationuser.Student_Name,
                    Student_Email = applicationuser.Student_Email,
                    Gender = applicationuser.Gender,
                    Students_Id = applicationuser.Students_Id,
                    FathersName = applicationuser.FathersName,
                    MothersName = applicationuser.MothersName,
                    Phone = applicationuser.Phone,
                    GuardianAddress = applicationuser.GuardianAddress,
                    LoginEmail = applicationuser.Student_Email,
                    LoginPassword = applicationuser.LoginPassword,
                    StudentClassId = applicationuser.StudentClassId,
                    ImgUrl = file!=null?@"\Images\Student\" + fileName:null
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
            var studentClass = _studentclassReposiroty.GetAll().Select(u => new SelectListItem
            {
                Value = u.StudentClassId.ToString(),
                Text = $"{u.Year} - {u.Semester}"
            }).ToList();

            ApplicationUser applicationUsers = _applicationUserReposiroty.Get(u => u.Id == id);
            StudentVM studentVM = new()
            {
                classList = studentClass,
                applicationUser  = applicationUsers,
            };
            return View(studentVM);
        }

        [HttpPost]
        public async Task<IActionResult> StudentEdit(StudentVM studentVm,IFormFile? file)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var studentFromDb = _applicationUserReposiroty.Get(u=>u.Id == studentVm.applicationUser.Id);
                    string fileName = "";
                    string oldImgPath;
                    var rootPath = _webHostEnvironment.WebRootPath;
                    var studentPath = Path.Combine(rootPath + @"\Images\Student\");
                    if (file != null)
                    {
                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                        if (!string.IsNullOrEmpty(studentFromDb.ImgUrl))
                        {
                            oldImgPath = Path.Combine(rootPath + studentFromDb.ImgUrl);
                            if (System.IO.File.Exists(oldImgPath))
                            {
                                System.IO.File.Delete(oldImgPath);
                            }
                        }
                        
                        using (var fileStream = new FileStream(Path.Combine(studentPath, fileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }

                    }

                    if(studentFromDb== null)
                    {
                        return NotFound();
                    }
                    studentFromDb.Email = studentVm.applicationUser.Student_Email;
                    studentFromDb.UserName = studentVm.applicationUser.Student_Email;
                    studentFromDb.Student_Name = studentVm.applicationUser.Student_Name;
                    studentFromDb.Student_Email = studentVm.applicationUser.Student_Email;
                    studentFromDb.Gender = studentVm.applicationUser.Gender;
                    studentFromDb.Students_Id = studentVm.applicationUser.Students_Id;
                    studentFromDb.FathersName = studentVm.applicationUser.FathersName;
                    studentFromDb.MothersName = studentVm.applicationUser.MothersName;
                    studentFromDb.Phone = studentVm.applicationUser.Phone;
                    studentFromDb.GuardianAddress = studentVm.applicationUser.GuardianAddress;
                    studentFromDb.StudentClassId = studentVm.applicationUser.StudentClassId;
                    studentFromDb.ImgUrl = file != null ? @"\Images\Student\" + fileName : studentFromDb.ImgUrl;
                    _applicationUserReposiroty.Update(studentFromDb);
                    _applicationUserReposiroty.SaveAsync();
                    return RedirectToAction("StudentIndex");
                }
                catch(DbUpdateConcurrencyException)
                {
                    if (!_applicationUserReposiroty.Exists(studentVm.applicationUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(studentVm);
            
        }
        [HttpPost]
        public async Task<IActionResult> StudentDelete(string? id)
        {
            var studenttobeDeleted = _applicationUserReposiroty.Get(u => u.Id == id);
            if(studenttobeDeleted == null)
            {
                return NotFound();
            }
            var rootPath = _webHostEnvironment.WebRootPath;
            var studentPath = Path.Combine(rootPath + @"\Images\Student\");
            if(!string.IsNullOrEmpty(studenttobeDeleted.ImgUrl))
            {
                var oldImgPath = Path.Combine(rootPath + studenttobeDeleted.ImgUrl);
                if (System.IO.File.Exists(oldImgPath))
                {
                    System.IO.File.Delete(oldImgPath);
                }
            }
            _applicationUserReposiroty.Remove(studenttobeDeleted);
            _applicationUserReposiroty.SaveAsync();

            return RedirectToAction("StudentIndex");
        }
    }
}
