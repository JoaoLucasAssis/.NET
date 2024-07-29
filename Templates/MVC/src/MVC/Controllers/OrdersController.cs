using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ValueObjects;

namespace MVC.Controllers
{
    [Route("orders")]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            ContextConnected();

            var applicationDbContext = _context.Order.Include(o => o.Client);
            return View(await applicationDbContext.ToListAsync());
        }

        [Route("details-order/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            ContextConnected();

            var order = await _context.Order
                .Include(o => o.Client)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [Route("create-order")]
        public IActionResult Create()
        {
            ContextConnected();

            ViewData["Status"] = OrderStatus.UnderReview;
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name");
            return View();
        }

        [HttpPost("create-order")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClientId,Status,Observation")] Order order)
        {
            ContextConnected();

            ModelState.Remove("StartDate");
            ModelState.Remove("EndDate");

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", order.ClientId);
            return View(order);
        }

        [Route("edit-order/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            ContextConnected();

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var status = OrderStatusOptions();
            ViewData["Status"] = new SelectList(status, "Id", "Name");
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", order.ClientId);

            return View(order);
        }

        [HttpPost("edit-order/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,Observation")] Order order)
        {
            ContextConnected();

            if (id != order.Id || !OrderExists(order.Id))
            {
                return NotFound();
            }

            var dbOrder = await _context.Order.FirstOrDefaultAsync(o => o.Id == id);
            if (dbOrder == null)
            {
                return NotFound();
            }
            dbOrder.Status = order.Status;
            dbOrder.Observation = order.Observation;
            
            if (ModelState.IsValid)
            {
                _context.Update(dbOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Name", order.ClientId);
            return View(order);
        }

        [Route("delete-order/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ContextConnected();

            var order = await _context.Order
                .Include(o => o.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost("delete-order/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ContextConnected();

            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        private void ContextConnected()
        {
            if (_context == null || _context.Order == null)
            {
                throw new InvalidOperationException("O contexto do banco de dados não está conectado ou a tabela Order não está acessível.");
            }
        }

        private IEnumerable<object> OrderStatusOptions()
        {
            var status = Enum
                .GetValues(typeof(OrderStatus))
                .Cast<OrderStatus>()
                .Select(s => new
                {
                    Id = (int)s,
                    Name = s.ToString()
                });
            return status;
        }
    }
}
