using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eshop.Domain.DomainModels;
using Eshop.Repository;
using Eshop.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Service.Implementation
{
    public class ProductInShoppingCartService : IProductInShoppingCartService
    {

        private readonly IRepository<ProductInShoppingCart> _repository;
        private readonly IShoppingCartService shoppingCartService;
        private readonly IProductService productService;
        private readonly ApplicationDbContext _context;

        public ProductInShoppingCartService(IRepository<ProductInShoppingCart> repository, 
            IShoppingCartService shoppingCartService,
            IProductService productService,ApplicationDbContext context)
        {
            _repository = repository;
            this.shoppingCartService = shoppingCartService;
            this.productService = productService;  
            _context = context;
        }


        public List<ProductInShoppingCart> GetAll()
        {
            return _repository.GetAll(x => x).ToList();    
        }

        public ProductInShoppingCart? GetById(Guid id)
        {
            return _repository.Get(selector:x => x,predicate: x=> x.Id==id);
        }

        public ProductInShoppingCart Add(ProductInShoppingCart product)
        {
            product.Id = Guid.NewGuid();
            return _repository.Insert(product);
        }

        public ProductInShoppingCart Update(ProductInShoppingCart product)
        {
            return _repository.Update(product);
        }
        public ProductInShoppingCart DeleteById(Guid id)
        {
            var product = GetById(id);
            if (product == null) throw new Exception("ProductInShoppingCart not found");
            _repository.Delete(product);
            return product;
        }

        public ProductInShoppingCart? GetByProductAndCart(Guid productId, Guid shoppingCartId)
        {
            return _repository.Get(selector: x => x, predicate: x => (x.ProductId == productId) && (x.ShoppingCartId == shoppingCartId));
        }

        public void UpdateCartItem(Product product, ShoppingCart shoppingCart)
        {
            var prodInCart = GetByProductAndCart(product.Id,shoppingCart.Id);

            if(prodInCart == null)
            {
                var newProdInCart = new ProductInShoppingCart(Guid.NewGuid(),product.Id, product, shoppingCart.Id, shoppingCart,1);

                _repository.Insert(newProdInCart);
            }
            else
            {
                prodInCart.Quantity += 1;
                _repository.Update(prodInCart);
            }


        }

        public void Create(string userId, Guid productId, int quantity)
        {

            var cart = this.shoppingCartService.GetByUserId(userId);

            if (cart == null) throw new Exception($"Shopping cart was not found for user ID: {userId}");

            var product = this.productService.GetById(productId);

            if (product == null) throw new Exception("Product was not found");

            var newProdInCart = new ProductInShoppingCart(Guid.NewGuid(),productId, product,cart.Id,cart,quantity);

            _repository.Insert(newProdInCart);
        }


        public void AddToCart(Guid prod_id, string userId)
        {
            var shoppingCart = shoppingCartService.GetByUserId(userId);

            if (shoppingCart == null)
            {
                var user = _context.Users.Select(x => x).Where(x => x.UserName == userId).FirstOrDefault();
                if (user == null) throw new Exception("Shopping Cart not found");
                else
                {
                    var newCart = new ShoppingCart(Guid.NewGuid(), userId, user);
                    shoppingCartService.Add(newCart);

                    user.ShoppingCart = newCart;
                    _context.Users.Update(user);
                    return;
                }

            }


            var product = productService.GetById(prod_id);
            if (product == null) throw new Exception("Product not found");

            UpdateCartItem(product, shoppingCart);


        }

    }
}
