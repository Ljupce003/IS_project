using System.Security.Claims;
using Eshop.Domain.DomainModels;
using Eshop.Domain.identity;
using Eshop.Repository;
using Eshop.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Eshop.web.Controllers
{
    public class ProductInShoppingCartsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<EshopUser> _userManager;
        private readonly IProductInShoppingCartService productInShoppingCartService;

        public ProductInShoppingCartsController(ApplicationDbContext context, UserManager<EshopUser> userManager, IProductInShoppingCartService productInShoppingCartService)
        {
            _context = context;
            _userManager = userManager;
            this.productInShoppingCartService = productInShoppingCartService;
        }

        // GET: ProductInShoppingCarts
        public async Task<IActionResult> Index()
        {

            var userId = _userManager.GetUserId(User);

            var user = await _context.Users
                .Include(u => u.ShoppingCart)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.ShoppingCart == null)
            {
                return NotFound();
            }

            var userCartId = user.ShoppingCart.Id;

            var applicationDbContext = _context.ProductInShoppingCarts
                .Include(p => p.Product)
                .Include(p => p.ShoppingCart)
                .Where(p => p.ShoppingCartId == userCartId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductInShoppingCarts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductInShoppingCarts
                .Include(p => p.Product)
                .Include(p => p.ShoppingCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }

            return View(productInShoppingCart);
        }

        // GET: ProductInShoppingCarts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName");
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "Id");
            return View();
        }

        // POST: ProductInShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,ProductId,Quantity")] ProductInShoppingCart productInShoppingCart)
        {
            //if (ModelState.IsValid)
            //{
            //    var userId = _userManager.GetUserId(User);

            //    var user = await _context.Users
            //        .Include(u => u.ShoppingCart)
            //        .FirstOrDefaultAsync(u => u.Id == userId);

            //    if (user == null || user.ShoppingCart == null)
            //    {
            //        return Unauthorized();
            //    }

            //    productInShoppingCart.ShoppingCartId = user.ShoppingCart.Id;
            //    productInShoppingCart.ShoppingCart = user.ShoppingCart;


            //    productInShoppingCart.Id = Guid.NewGuid();
            //    _context.Add(productInShoppingCart);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", productInShoppingCart.ProductId);
            //ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "Id", productInShoppingCart.ShoppingCartId);
            //return View(productInShoppingCart);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return BadRequest();
            }
            productInShoppingCartService.Create(userId, productInShoppingCart.ProductId, productInShoppingCart.Quantity);

            return RedirectToAction(nameof(Index));
        }

        // GET: ProductInShoppingCarts/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductInShoppingCarts.FindAsync(id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", productInShoppingCart.ProductId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "Id", productInShoppingCart.ShoppingCartId);
            return View(productInShoppingCart);
        }

        // POST: ProductInShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductId,Quantity")] ProductInShoppingCart productInShoppingCart)
        {
            if (id != productInShoppingCart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var userId = _userManager.GetUserId(User);

                var user = await _context.Users
                    .Include(u => u.ShoppingCart)
                    .FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null || user.ShoppingCart == null)
                {
                    return NotFound();
                }

                productInShoppingCart.ShoppingCartId = user.ShoppingCart.Id;
                productInShoppingCart.ShoppingCart = user.ShoppingCart;


                try
                {
                    _context.Update(productInShoppingCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInShoppingCartExists(productInShoppingCart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "ProductName", productInShoppingCart.ProductId);
            ViewData["ShoppingCartId"] = new SelectList(_context.ShoppingCarts, "Id", "Id", productInShoppingCart.ShoppingCartId);
            return View(productInShoppingCart);
        }

        // GET: ProductInShoppingCarts/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInShoppingCart = await _context.ProductInShoppingCarts
                .Include(p => p.Product)
                .Include(p => p.ShoppingCart)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productInShoppingCart == null)
            {
                return NotFound();
            }

            return View(productInShoppingCart);
        }

        // POST: ProductInShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productInShoppingCart = await _context.ProductInShoppingCarts.FindAsync(id);
            if (productInShoppingCart != null)
            {
                _context.ProductInShoppingCarts.Remove(productInShoppingCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInShoppingCartExists(Guid id)
        {
            return _context.ProductInShoppingCarts.Any(e => e.Id == id);
        }
    }
}
