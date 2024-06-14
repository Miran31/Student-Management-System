using System.Linq.Expressions;

namespace StudentManagementSystem.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T,bool>> filter);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> values);


    }
}
