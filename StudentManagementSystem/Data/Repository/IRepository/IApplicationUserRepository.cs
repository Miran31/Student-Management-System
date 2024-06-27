using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data.Repository.IRepository
{
    public interface IApplicationUserRepository: IRepository<ApplicationUser>
    {
        void Update(ApplicationUser applicationUser);
        void Save();
        void SaveAsync();
        bool Exists(string id);
    }
}
