using Eshop.Domain.DomainModels;
using Eshop.Domain.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Repository;

public class ApplicationDbContext : IdentityDbContext<EshopUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
}
