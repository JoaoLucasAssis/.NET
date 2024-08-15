using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Areas.Inventory.Models;
using MVC.Data;

namespace MVC.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    [Route("stocks")]
    [Authorize]

    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stock.Include(s => s.Store);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("{id:int}/details-stock")]
        public async Task<IActionResult> Details(int id)
        {
            var stock = await _context.Stock
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        [Route("add-stock")]
        public IActionResult Create()
        {
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name");
            return View();
        }

        [HttpPost("add-stock")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Qty,StoreId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Name", stock.StoreId);
            return View(stock);
        }

        [Route("{id:int}/edit-stock")]
        public async Task<IActionResult> Edit(int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Location", stock.StoreId);
            return View(stock);
        }

        [HttpPost("{id:int}/edit-stock")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Qty,StoreId")] Stock stock)
        {
            if (id != stock.Id || !StockExists(stock.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(stock);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["StoreId"] = new SelectList(_context.Store, "Id", "Location", stock.StoreId);
            return View(stock);
        }

        [Route("delete-stock/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _context.Stock
                .Include(s => s.Store)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        [HttpPost("delete-stock/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock != null)
            {
                _context.Stock.Remove(stock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.Stock.Any(e => e.Id == id);
        }
    }
}
