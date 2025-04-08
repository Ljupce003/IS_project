using Eshop.Domain.DomainModels;
using Eshop.Repository;
using Eshop.Service.Interface;

namespace Eshop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repository;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ApplicationDbContext _context;


        public ProductService(IRepository<Product> repository,
            IShoppingCartService shoppingCartService,
            ApplicationDbContext context)
        {
            _repository = repository;
            _shoppingCartService = shoppingCartService;
            _context = context;
        }


        public List<Product> GetAll()
        {
            return _repository.GetAll(selector: x => x).ToList();
        }

        public Product? GetById(Guid id)
        {
            return _repository.Get(selector: x => x, predicate: x => x.Id == id);
        }

        public Product Add(Product product)
        {
            product.Id = Guid.NewGuid();
            return _repository.Insert(product);
        }

        public Product Update(Product product)
        {
            return _repository.Update(product);
        }
        public Product DeleteById(Guid id)
        {
            var product = GetById(id);
            if (product == null) throw new Exception("Product not found");
            _repository.Delete(product);
            return product;
        }


    }
}
