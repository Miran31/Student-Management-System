using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using StudentManagementSystem.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Students_Id { get; set; }

        public string? Student_Name { get; set; }

        public string Student_Email { get; set; }
        public string? Gender { get; set; }
        public string? ImgUrl { get; set; }
        public string? DOB { get; set; }

        public string? FathersName { get; set; }

        public string? MothersName { get; set; }
        public string? Contact_Number { get; set; }
        public string? GuardianAddress { get; set; }
        public string? LoginEmail { get; set; }
        public string? LoginPassword { get; set; }
    }
}
