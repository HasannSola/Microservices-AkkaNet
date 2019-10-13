using Microsoft.EntityFrameworkCore;
using MSA.Entities.Interfaces;
using MSA.Entities.Model;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MSA.Core.DataAccess.EntityFramework
{
    public abstract class EfEntityRepositoryBase<TEntity, TContext> : IRepository<TEntity>
           where TEntity : class, IEntity, new()
           where TContext : DbContext, new()
    {
        public virtual CResult<TEntity> Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var AddEntity = context.Entry(entity);
                AddEntity.State = EntityState.Added;
                context.SaveChanges();
                return new CResult<TEntity> { Object = entity, Succeed = true, Message = "Kayıt İşlemi Başarılı" };
            }
        }

        public virtual CResult<TEntity> Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var DeleteEntity = context.Entry(entity);
                DeleteEntity.State = EntityState.Deleted;
                context.SaveChanges();
                return new CResult<TEntity> { Object = null, Message = "Kayıt Silme işlemi" };
            }
        }

        public virtual CResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return new CResult<TEntity> { Object = context.Set<TEntity>().SingleOrDefault(filter) };
            }
        }

        public virtual CResult<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                var list = filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
                return new CResult<TEntity> { Objects = list, Object = null, Succeed = true, Message = "Başarılı" };
            }
        }

        public bool IsCodeInUse(string code)
        {
            throw new NotImplementedException();
        }

        public virtual CResult<TEntity> Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var UpdateEntity = context.Entry(entity);
                UpdateEntity.State = EntityState.Modified;
                context.SaveChanges();
                return new CResult<TEntity> { Objects = null, Object = entity, Succeed = true, Message = "Güncelleme İşlemi Başarılı" };
            }
        }
    }
}
