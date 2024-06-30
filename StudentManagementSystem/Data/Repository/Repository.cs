using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Data.Repository.IRepository;
using System.Linq.Expressions;

namespace StudentManagementSystem.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbset;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbset = _db.Set<T>();
            _db.applicationUsers.Include(u=>u.StudentClass).Include(u=>u.StudentClassId);

        }
        public void Add(T entity)
        {
            dbset.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProterty = null)
        {
            IQueryable<T> query = dbset.Where(filter);
            if (!string.IsNullOrEmpty(includeProterty))
            {
                query = query.Include(includeProterty);
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProterty = null)
        {
            IQueryable<T> query = dbset;
            if (!string.IsNullOrEmpty(includeProterty))
            {
                foreach(var includProp in includeProterty.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includProp);

                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbset.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> values)
        {
            dbset.RemoveRange(values);
        }
    }
}
