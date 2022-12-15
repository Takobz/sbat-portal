using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SBAT.Domain.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        T? GetById(int id);
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        //update the whole entry's columns
        //if you want to update one field I advise to add a method in the desired repository and use EF Attach() method
        //reference: https://stackoverflow.com/questions/30987806/dbset-attachentity-vs-dbcontext-entryentity-state-entitystate-modified
        void Update(T entity); 
    }

    public class EntityBase 
    {
        public int Id { get; private set; }
    }
}