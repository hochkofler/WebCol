using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCol.Data;
using WebCol.Models;

namespace WebCol.Controllers
{
    public class ProductoPrincipiosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoPrincipiosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProductoPrincipios
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductosPrincipios.Include(p => p.Principio).Include(p => p.Producto);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductoPrincipios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPrincipio = await _context.ProductosPrincipios
                .Include(p => p.Principio)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (productoPrincipio == null)
            {
                return NotFound();
            }

            return View(productoPrincipio);
        }

        // GET: ProductoPrincipios/Create
        public IActionResult Create()
        {
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: ProductoPrincipios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductoId,PrincipioId")] ProductoPrincipio productoPrincipio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productoPrincipio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", productoPrincipio.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPrincipio.ProductoId);
            return View(productoPrincipio);
        }

        // GET: ProductoPrincipios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPrincipio = await _context.ProductosPrincipios.FindAsync(id);
            if (productoPrincipio == null)
            {
                return NotFound();
            }
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", productoPrincipio.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPrincipio.ProductoId);
            return View(productoPrincipio);
        }

        // POST: ProductoPrincipios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,PrincipioId")] ProductoPrincipio productoPrincipio)
        {
            if (id != productoPrincipio.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productoPrincipio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoPrincipioExists(productoPrincipio.ProductoId))
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
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", productoPrincipio.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", productoPrincipio.ProductoId);
            return View(productoPrincipio);
        }

        // GET: ProductoPrincipios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productoPrincipio = await _context.ProductosPrincipios
                .Include(p => p.Principio)
                .Include(p => p.Producto)
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (productoPrincipio == null)
            {
                return NotFound();
            }

            return View(productoPrincipio);
        }

        // POST: ProductoPrincipios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productoPrincipio = await _context.ProductosPrincipios.FindAsync(id);
            if (productoPrincipio != null)
            {
                _context.ProductosPrincipios.Remove(productoPrincipio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoPrincipioExists(int id)
        {
            return _context.ProductosPrincipios.Any(e => e.ProductoId == id);
        }
    }
}
