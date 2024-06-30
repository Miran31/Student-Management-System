using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentManagementSystem.Models.Viewmodel
{
    public class StudentVM
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> classList { get; set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
