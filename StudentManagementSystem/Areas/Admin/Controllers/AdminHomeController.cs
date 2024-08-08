using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Data.Repository.IRepository;
using StudentManagementSystem.Models;
using StudentManagementSystem.Models.Viewmodel;
using StudentManagementSystem.Utility;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IStudentclassRepository _studentClassRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdminHomeController(IStudentclassRepository studentclassRepository, IApplicationUserRepository applicationUserRepository,UserManager<ApplicationUser> userManager)
        {
            _studentClassRepository = studentclassRepository;
            _applicationUserRepository = applicationUserRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<StudentClass> studentClass = _studentClassRepository.GetAll().ToList();
            List<ApplicationUser> applicationUsers = _applicationUserRepository.GetAll().ToList();
            List<ApplicationUser> finalStudent =  new List<ApplicationUser>();
            foreach(var user in applicationUsers)
            {
                var role = await _userManager.GetRolesAsync(user);
                if(role.Contains( SD.Role_student))
                {
                    finalStudent.Add(user);
                }
            }
            AdminVM adminVM = new()
            {
                ClassCount = studentClass.Count,
                StudentCount = finalStudent.Count
            };
            return View(adminVM);
        }
    }
}
