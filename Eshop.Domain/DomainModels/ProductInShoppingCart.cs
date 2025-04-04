using System.ComponentModel.DataAnnotations;
using Eshop.Domain.DomainModels;

namespace Eshop.Domain.DomainModels
{
    public class ProductInShoppingCart : BaseEntity
    {
        public ProductInShoppingCart(Guid id) : base(id)
        {
        }

        public ProductInShoppingCart() : base(Guid.Empty){ }

        public ProductInShoppingCart(Guid productId, Product? product, Guid shoppingCartId, ShoppingCart? shoppingCart, int quantity) : base(productId)
        {
            ProductId = productId;
            Product = product;
            ShoppingCartId = shoppingCartId;
            ShoppingCart = shoppingCart;
            Quantity = quantity;
        }
        public ProductInShoppingCart(Guid id,Guid productId, Product? product, Guid shoppingCartId, ShoppingCart? shoppingCart, int quantity) : base(id)
        {
            ProductId = productId;
            Product = product;
            ShoppingCartId = shoppingCartId;
            ShoppingCart = shoppingCart;
            Quantity = quantity;
        }




        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid ShoppingCartId { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public int Quantity { get; set; }


    }
}
