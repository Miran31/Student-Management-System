using StudentManagementSystem.Areas.Admin.Models;

namespace StudentManagementSystem.Data.Repository.IRepository
{
    public interface IStudentclassRepository:IRepository<StudentClass>
    {
        void Update(StudentClass studentClass);
        void Save();
    }
}
