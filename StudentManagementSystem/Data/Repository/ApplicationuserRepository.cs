using StudentManagementSystem.Data.Repository.IRepository;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data.Repository
{
    public class ApplicationuserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationuserRepository(ApplicationDbContext applicationDbContext): base(applicationDbContext)
        {
            _context = applicationDbContext;
            
        }

        public bool Exists(string id)
        {
            return _context.applicationUsers.Any(u => u.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        public void Update(ApplicationUser applicationUser)
        {
            _context.Update(applicationUser);
        }
    }
}
