using System.ComponentModel.DataAnnotations;

namespace Eshop.Domain.DomainModels
{
    public class Product : BaseEntity
    {
        public Product(Guid id) : base(id)
        {
        }
        public Product(): base(Guid.Empty) { }

        public Product(Guid id, string? productName,
            string? productImage,
            string? productDescription, 
            int rating, 
            int productPrice, 
            ICollection<ProductInShoppingCart>? productInShoppingCarts) : base(id)
        {
            ProductName = productName;
            ProductImage = productImage;
            ProductDescription = productDescription;
            Rating = rating;
            ProductPrice = productPrice;
            ProductInShoppingCarts = productInShoppingCarts;
        }

        public Product(string? productName, string? productImage, string? productDescription, int rating, int productPrice) :base(Guid.NewGuid())
        {
            ProductName = productName;
            ProductImage = productImage;
            ProductDescription = productDescription;
            Rating = rating;
            ProductPrice = productPrice;
        }

        [Required]
        public string? ProductName { get; set; }
        [Required]
        public string? ProductImage { get; set; }
        [Required]
        public string? ProductDescription { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public int ProductPrice { get; set; }

        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
    }
}
