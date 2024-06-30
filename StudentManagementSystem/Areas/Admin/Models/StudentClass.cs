using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Areas.Admin.Models
{
    public class StudentClass
    {
        [Key] public int StudentClassId { get; set; }
        [Required]
        [Range(1,10)]
        public int Year { get; set; }
        [Required]
        [Range(1,2)]

        public int Semester { get; set; }
    }
}
