using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Areas.Admin.Models
{
    public class StudentClass
    {
        public int StudentClassId { get; set; }
        [Required]
        public string Session {  get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Semester { get; set; }
    }
}
