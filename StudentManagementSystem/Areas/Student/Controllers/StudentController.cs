using Microsoft.AspNetCore.Mvc;

namespace StudentManagementSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
