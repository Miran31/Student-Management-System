using System.Linq.Expressions;

namespace StudentManagementSystem.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        IEnumerable<T> GetAll(string ? includeProterty = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProterty = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> values);


    }
}
