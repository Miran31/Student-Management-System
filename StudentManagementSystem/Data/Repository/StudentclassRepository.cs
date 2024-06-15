using StudentManagementSystem.Areas.Admin.Models;
using StudentManagementSystem.Data.Repository.IRepository;

namespace StudentManagementSystem.Data.Repository
{
    public class StudentclassRepository : Repository<StudentClass>, IStudentclassRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentclassRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            _context = dbContext;
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(StudentClass studentClass)
        {
            _context.Update(studentClass);
        }
    }
}
