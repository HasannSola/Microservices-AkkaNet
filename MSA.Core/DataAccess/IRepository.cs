using MSA.Entities.Interfaces;
using MSA.Entities.Model;
using System;
using System.Linq.Expressions;

namespace MSA.Core.DataAccess
{
    public interface IRepository<T> where T : class, IEntity, new()
    {
        CResult<T> GetAll(Expression<Func<T, bool>> filter = null);
        CResult<T> Get(Expression<Func<T, bool>> filter);
        CResult<T> Add(T entity);
        CResult<T> Update(T entity);
        CResult<T> Delete(T entity);
        bool IsCodeInUse(string code);
    }
}
