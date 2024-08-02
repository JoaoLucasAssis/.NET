using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC.Data;
using MVC.Models;
using MVC.ValueObjects;

namespace MVC.Controllers
{
    [Route("clients")]
    [Authorize]
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string? searchTerm)
        {
            ViewData["SearchTerm"] = searchTerm;
            var filteredClients = await GetFilteredClientsAsync(searchTerm);
            return View(filteredClients);
        }

        [Route("details-client/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            ContextConnected();

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [Route("create-client")]
        public IActionResult Create()
        {
            var states = StatesOptions();
            ViewData["States"] = new SelectList(states, "Id", "Code");

            return View();
        }

        [HttpPost("create-client")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,CEP,State,City")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Operation completed successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [Route("edit-client/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            ContextConnected();

            var states = StatesOptions();
            ViewData["States"] = new SelectList(states, "Id", "Code");

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        [HttpPost("edit-client/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,CEP,State,City")] Client client)
        {
            if (id != client.Id || !ClientExists(client.Id))
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _context.Update(client);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Operation completed successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [Route("delete-client/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ContextConnected();

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        [HttpPost("delete-client/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = "Operation completed successfully!";
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.Id == id);
        }

        private void ContextConnected()
        {
            if (_context == null || _context.Client == null)
            {
                throw new InvalidOperationException("O contexto do banco de dados não está conectado ou a tabela Client não está acessível.");
            }
        }

        private IEnumerable<object> StatesOptions()
        {
            var states = Enum
                .GetValues(typeof(BrazilianStates))
                .Cast<BrazilianStates>()
                .Select(s => new 
                {
                    Id = (int)s,
                    Code = s.ToString()
                });
            return states;
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
}
