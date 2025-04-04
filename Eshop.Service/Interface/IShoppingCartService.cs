using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.DomainModels;

namespace Eshop.Service.Interface
{
    public interface IShoppingCartService
    {
        List<ShoppingCart> GetAll();
        ShoppingCart? GetById(Guid id);
        ShoppingCart? GetByUserId(string id);
        ShoppingCart Add(ShoppingCart cart);
        ShoppingCart Update(ShoppingCart cart);
        ShoppingCart DeleteById(Guid id);
    }
}
