using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Eshop.Domain.DomainModels;
using Eshop.Repository;
using Microsoft.AspNetCore.Identity;
using Eshop.Domain.identity;
using Eshop.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Eshop.web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly UserManager<EshopUser> _userManager;
        private readonly IProductService productService;
        private readonly IProductInShoppingCartService productInShoppingCartService;

        public ProductsController(UserManager<EshopUser> userManager, 
            IProductService productService,IProductInShoppingCartService prodInshoppingCartService)
        {
            _userManager = userManager;
            this.productService = productService;
            this.productInShoppingCartService = prodInshoppingCartService;
        }



        // GET: Products
        public IActionResult Index()
        {
            return View(productService.GetAll());
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            
            //MVC
            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);

            if(id == null) return BadRequest();

            var product = productService.GetById((Guid)id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductName,ProductImage,ProductDescription,Rating,ProductPrice")] Product product)
        {
            //if (ModelState.IsValid)
            //{
            //    product.Id = Guid.NewGuid();
            //    _context.Add(product);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            var prod = productService.Add(product);

            if (prod == null) return BadRequest();

            return RedirectToAction(nameof(Index)); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public  IActionResult AddToCart(Guid productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null) {
                return BadRequest();
            }

            
            productInShoppingCartService.AddToCart(productId, userId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //var product = await _context.Products.FindAsync(id);
            //if (product == null)
            //{
            //    return NotFound();
            //}
            var product = this.productService.GetById((Guid)id);

            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("id,ProductName,ProductImage,ProductDescription,Rating,ProductPrice")] Product product)
        {
            if (id != product.Id)
            {
                product.Id = id;
            }

            //MVC
            //if (ModelState.IsValid)
            //{
            //    try
            //    {
            //        _context.Update(product);
            //        await _context.SaveChangesAsync();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        if (!ProductExists(product.Id))
            //        {
            //            return NotFound();
            //        }
            //        else
            //        {
            //            throw;
            //        }
            //    }
            //    return RedirectToAction(nameof(Index));
            //}

            productService.Update(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            //var product = await _context.Products
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var product = productService.GetById((Guid)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(Guid id)
        {
            //var product = await _context.Products.FindAsync(id);
            //if (product != null)
            //{
            //    _context.Products.Remove(product);
            //}

            //await _context.SaveChangesAsync();

            productService.DeleteById(id);

            return RedirectToAction(nameof(Index));
        }


    }
}
