using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.DomainModels;

namespace Eshop.Service.Interface
{
    public interface IProductService
    {

        List<Product> GetAll();
        Product? GetById(Guid id);
        Product Add(Product product);
        Product Update(Product product);
        Product DeleteById(Guid id);
        //void AddToCart(Guid prod_id,string userId);




    }
}
