using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.DomainModels;
using Eshop.Repository;
using Eshop.Service.Interface;

namespace Eshop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }


        public List<ShoppingCart> GetAll()
        {
            return _shoppingCartRepository.GetAll(selector:x => x).ToList();
        }

        public ShoppingCart? GetById(Guid id)
        {
            return _shoppingCartRepository.Get(selector: x => x,predicate: x => x.Id == id);
        }

        public ShoppingCart Add(ShoppingCart cart)
        {
            cart.Id = Guid.NewGuid();
            return _shoppingCartRepository.Insert(cart);
        }

        public ShoppingCart Update(ShoppingCart cart)
        {
            return _shoppingCartRepository.Update(cart);
        }
        public ShoppingCart DeleteById(Guid id)
        {
            var cart = GetById(id);
            if (cart == null) throw new Exception("Shopping Cart not found");

            _shoppingCartRepository.Delete(cart);
            return cart;
        }

        public ShoppingCart? GetByUserId(string ownerId)
        {
            return _shoppingCartRepository
                .Get(selector: x => x, 
                predicate: x => x.OwnerId == ownerId);
        }
    }
}
