using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ValueObjects;

namespace MVC.Controllers
{
    [Route("products")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;
            ViewData["Controller"] = "products";

            var filteredProducts = await GetFilteredProductsAsync(searchTerm);
            return View(filteredProducts);
        }

        [Route("details-product/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            ContextConnected();

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Route("create-product")]
        public async Task<IActionResult> Create()
        {
            var stocks = await _context.Stock.ToListAsync();
            var types = ProductTypesOptions();
            ViewData["Stocks"] = new SelectList(stocks, "Id", "Id");
            ViewData["Types"] = new SelectList(types, "Id", "Type");

            return View();
        }

        [HttpPost("create-product")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,ProductType,StockId")] Product product)
        {
            ModelState.Remove("Qty");
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Operation completed successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("edit-product/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            ContextConnected();

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var stocks = await _context.Product.ToListAsync();
            var types = ProductTypesOptions();
            ViewData["Stocks"] = new SelectList(stocks, "Id", "Id");
            ViewData["Types"] = new SelectList(types, "Id", "Type");

            return View(product);
        }

        [HttpPost("edit-product/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,ProductType,StockId")] Product product)
        {
            if (id != product.Id || !ProductExists(product.Id))
            {
                return NotFound();
            }

            ModelState.Remove("Qty");
            if (ModelState.IsValid)
            {
               _context.Update(product);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Operation completed successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Route("delete-product/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ContextConnected();

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost("delete-product/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Operation completed successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        private void ContextConnected()
        {
            if (_context == null || _context.Product == null)
            {
                throw new InvalidOperationException("O contexto do banco de dados não está conectado ou a tabela Product não está acessível.");
            }
        }

        private IEnumerable<object> ProductTypesOptions()
        {
            var types = Enum
                .GetValues(typeof(ProductType))
                .Cast<ProductType>()
                .Select(s => new
                {
                    Id = (int)s,
                    Type = s.ToString()
                });
            return types;
        }

        private async Task<IEnumerable<Product>> GetFilteredProductsAsync(string searchTerm)
        {
            if (searchTerm is null)
            {
                return await _context.Product.ToListAsync();
            }
            else
            {
                return await _context.Product
                    .Where(p => p.Name.Contains(searchTerm))
                    .ToListAsync();
            }
        }
    }
}
