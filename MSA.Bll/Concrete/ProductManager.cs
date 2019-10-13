using MSA.Bll.Abstract;
using MSA.Dal.Abstract;
using MSA.Entities.Entities;
using Newtonsoft.Json;
using System;

namespace MSA.Bll.Concrete
{
    public class ProductManager : IProductManager
    {
        private IProductDal _procductDal;
        public ProductManager(IProductDal procductDal)
        {
            _procductDal = procductDal;
        }

        public string Add(Product product)
        {
            product.CreateDate = DateTime.Now;
            var result = _procductDal.Add(product);
            return JsonConvert.SerializeObject(result);
        }

        public string GetAll()
        {
            return JsonConvert.SerializeObject(_procductDal.GetAll());
        }

        public string Update(Product product)
        {
            product.CreateDate = DateTime.Now;
            return JsonConvert.SerializeObject(_procductDal.Update(product));
        }
    }
}
