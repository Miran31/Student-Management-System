using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Utility;

namespace StudentManagementSystem.Areas.Admin.Controllers
{
    [Area("Studentclass")]
    [Authorize(SD.Role_admin)]
    public class StudentclassController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Index() { 
            return View();
        }
    }
}
