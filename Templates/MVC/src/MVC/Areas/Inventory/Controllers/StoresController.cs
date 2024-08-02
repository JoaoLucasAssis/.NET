using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Areas.Inventory.Models;
using MVC.Data;

namespace MVC.Areas.Inventory.Controllers
{
    [Area("Inventory")]
    [Route("stores")]
    [Authorize]
    public class StoresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Store.ToListAsync());
        }

        [Route("{id:int}/details-store")]
        public async Task<IActionResult> Details(int id)
        {
            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [Route("add-store")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("add-store")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Location")] Store store)
        {
            if (ModelState.IsValid)
            {
                _context.Add(store);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        [Route("{id:int}/edit-store")]
        public async Task<IActionResult> Edit(int id)
        {
            var store = await _context.Store.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }

        [HttpPost("{id:int}/edit-store")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Location")] Store store)
        {
            if (id != store.Id || !StoreExists(store.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(store);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(store);
        }

        [Route("{id:int}/delete-store")]
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _context.Store
                .FirstOrDefaultAsync(m => m.Id == id);
            if (store == null)
            {
                return NotFound();
            }

            return View(store);
        }

        [HttpPost("{id:int}/delete-store"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var store = await _context.Store.FindAsync(id);
            if (store != null)
            {
                _context.Store.Remove(store);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoreExists(int id)
        {
            return _context.Store.Any(e => e.Id == id);
        }
    }
}
