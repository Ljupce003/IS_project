using System.ComponentModel.DataAnnotations;
using Eshop.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Domain.identity
{
    public class EshopUser : IdentityUser

    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? Address { get; set; }

        public virtual ShoppingCart? ShoppingCart { get; set; }
    }
}
