using MSA.Core.DataAccess.EntityFramework;
using MSA.Dal.Abstract;
using MSA.Entities.Entities;

namespace MSA.Dal.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, MSADbContext>, IProductDal
    {

    }
}
