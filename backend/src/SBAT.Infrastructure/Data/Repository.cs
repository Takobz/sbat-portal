using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SBAT.Core.Interfaces;

namespace SBAT.Infrastructure.Data
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly SBATDbContext _dbContext;
        public Repository(SBATDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public T? GetById(int id)
        {
           return _dbContext.Set<T>().Find(id);
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                .Where(predicate)
                .FirstOrDefault();
        }

        public IEnumerable<T> List()
        {
            return _dbContext.Set<T>().AsEnumerable();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>()
                    .Where(predicate)
                    .AsEnumerable();
        }
    }
}