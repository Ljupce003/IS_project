using System.ComponentModel.DataAnnotations;
using Eshop.Domain.identity;

namespace Eshop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public ShoppingCart(Guid id) : base(id){
        }

        public ShoppingCart(Guid id, 
            string? ownerId,
            EshopUser? owner,
            ICollection<ProductInShoppingCart>? productInShoppingCarts) : base(id)
        {
            OwnerId = ownerId;
            Owner = owner;
            ProductInShoppingCarts = productInShoppingCarts;
        }

        public ShoppingCart(Guid id,string? ownerId, EshopUser? owner): base(id)
        {
            OwnerId = ownerId;
            Owner = owner;
        }

        public string? OwnerId { get; set; }
        public EshopUser? Owner { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
    }
}
