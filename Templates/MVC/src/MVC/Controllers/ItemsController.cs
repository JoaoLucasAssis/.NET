using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;

namespace MVC.Controllers;

[Route("items")]
[Authorize]
public class ItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Item.Include(i => i.Order).Include(i => i.Product);
        return View(await applicationDbContext.ToListAsync());
    }

    [Route("details-item/{id:int}")]
    public async Task<IActionResult> Details(int id)
    {
        ContextConnected();

        var item = await _context.Item
            .Include(i => i.Order)
            .Include(i => i.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [Route("add-item")]
    public IActionResult Create()
    {
        ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id");
        ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
        return View();
    }

    [HttpPost("add-item")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,OrderId,ProductId,Qty")] Item item)
    {
        if (ModelState.IsValid)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Operation completed successfully!";
            return RedirectToAction(nameof(Index));
        }
        ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", item.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", item.ProductId);
        return View(item);
    }

    [Route("edit-item/{id:int}")]
    public async Task<IActionResult> Edit(int id)
    {
        ContextConnected();

        var item = await _context.Item.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        ViewData["OrderId"] = new SelectList(_context.Order, "Id", "Id", item.OrderId);
        ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", item.ProductId);
        return View(item);
    }

    [HttpPost("edit-item/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Qty")] Item item)
    {
        if (id != item.Id || !ItemExists(item.Id))
        {
            return NotFound();
        }

        var dbItem = await _context.Item.FirstOrDefaultAsync(i => i.Id == id);

        if (dbItem == null)
        {
            return NotFound();
        }

        dbItem.Qty = item.Qty;

        if (ModelState.IsValid)
        {
            _context.Update(dbItem);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Operation completed successfully!";
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    [Route("delete-item/{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        if (_context == null)
        {
            return NotFound();
        }

        var item = await _context.Item
            .Include(i => i.Order)
            .Include(i => i.Product)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    [HttpPost("delete-item/{id:int}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var item = await _context.Item.FindAsync(id);
        if (item != null)
        {
            _context.Item.Remove(item);
        }

        await _context.SaveChangesAsync();
        TempData["Success"] = "Operation completed successfully!";
        return RedirectToAction(nameof(Index));
    }

    private bool ItemExists(int id)
    {
        return _context.Item.Any(e => e.Id == id);
    }

    private void ContextConnected()
    {
        if (_context == null || _context.Item == null)
        {
            throw new InvalidOperationException("O contexto do banco de dados não está conectado ou a tabela Client não está acessível.");
        }
    }

    private async Task<IEnumerable<Client>> GetFilteredClientsAsync(string searchTerm)
    {
        if (searchTerm is null)
        {
            return await _context.Client.ToListAsync();
        }
        else
        {
            return await _context.Client
                .Where(c => c.Name.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
