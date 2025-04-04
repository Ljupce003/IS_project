using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.DomainModels;

namespace Eshop.Service.Interface
{
    public interface IProductInShoppingCartService
    {
        List<ProductInShoppingCart> GetAll();
        ProductInShoppingCart? GetById(Guid id);

        ProductInShoppingCart? GetByProductAndCart(Guid productId,Guid shoppingCartId);
        ProductInShoppingCart Add(ProductInShoppingCart product);
        ProductInShoppingCart Update(ProductInShoppingCart product);
        ProductInShoppingCart DeleteById(Guid id);
        void UpdateCartItem(Product product, ShoppingCart shoppingCart);
        void Create(string userId, Guid productId, int quantity);
        public void AddToCart(Guid prod_id, string userId);
    }
}
