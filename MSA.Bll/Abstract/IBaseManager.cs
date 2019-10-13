using MSA.Entities.Interfaces;

namespace MSA.Bll.Abstract
{
    public interface IBaseManager<T> where T : class, IEntity, new()
    {
        string Add(T model);
        string Update(T model);
        string GetAll();
    }
}
