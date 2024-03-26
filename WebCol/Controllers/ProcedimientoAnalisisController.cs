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
    public class ProcedimientoAnalisisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProcedimientoAnalisisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProcedimientoAnalisis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProcedimientosAnalisis.Include(p => p.FaseMovil).Include(p => p.Principio).Include(p => p.Producto).Include(p => p.ProductoPrincipio);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProcedimientoAnalisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoAnalisis = await _context.ProcedimientosAnalisis
                .Include(p => p.FaseMovil)
                .Include(p => p.Principio)
                .Include(p => p.Producto)
                .Include(p => p.ProductoPrincipio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimientoAnalisis == null)
            {
                return NotFound();
            }

            return View(procedimientoAnalisis);
        }

        // GET: ProcedimientoAnalisis/Create
        public IActionResult Create()
        {
            ViewData["FaseMovilId"] = new SelectList(_context.FasesMoviles, "Id", "Id");
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.ProductosPrincipios, "ProductoId", "ProductoId");
            return View();
        }

        // POST: ProcedimientoAnalisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FaseMovilId,Tipo,ProductoId,PrincipioId")] ProcedimientoAnalisis procedimientoAnalisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procedimientoAnalisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FaseMovilId"] = new SelectList(_context.FasesMoviles, "Id", "Id", procedimientoAnalisis.FaseMovilId);
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", procedimientoAnalisis.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", procedimientoAnalisis.ProductoId);
            ViewData["ProductoId"] = new SelectList(_context.ProductosPrincipios, "ProductoId", "ProductoId", procedimientoAnalisis.ProductoId);
            return View(procedimientoAnalisis);
        }

        // GET: ProcedimientoAnalisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoAnalisis = await _context.ProcedimientosAnalisis.FindAsync(id);
            if (procedimientoAnalisis == null)
            {
                return NotFound();
            }
            ViewData["FaseMovilId"] = new SelectList(_context.FasesMoviles, "Id", "Id", procedimientoAnalisis.FaseMovilId);
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", procedimientoAnalisis.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", procedimientoAnalisis.ProductoId);
            ViewData["ProductoId"] = new SelectList(_context.ProductosPrincipios, "ProductoId", "ProductoId", procedimientoAnalisis.ProductoId);
            return View(procedimientoAnalisis);
        }

        // POST: ProcedimientoAnalisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FaseMovilId,Tipo,ProductoId,PrincipioId")] ProcedimientoAnalisis procedimientoAnalisis)
        {
            if (id != procedimientoAnalisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procedimientoAnalisis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcedimientoAnalisisExists(procedimientoAnalisis.Id))
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
            ViewData["FaseMovilId"] = new SelectList(_context.FasesMoviles, "Id", "Id", procedimientoAnalisis.FaseMovilId);
            ViewData["PrincipioId"] = new SelectList(_context.Principios, "Id", "Id", procedimientoAnalisis.PrincipioId);
            ViewData["ProductoId"] = new SelectList(_context.Productos, "Id", "Id", procedimientoAnalisis.ProductoId);
            ViewData["ProductoId"] = new SelectList(_context.ProductosPrincipios, "ProductoId", "ProductoId", procedimientoAnalisis.ProductoId);
            return View(procedimientoAnalisis);
        }

        // GET: ProcedimientoAnalisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procedimientoAnalisis = await _context.ProcedimientosAnalisis
                .Include(p => p.FaseMovil)
                .Include(p => p.Principio)
                .Include(p => p.Producto)
                .Include(p => p.ProductoPrincipio)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (procedimientoAnalisis == null)
            {
                return NotFound();
            }

            return View(procedimientoAnalisis);
        }

        // POST: ProcedimientoAnalisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var procedimientoAnalisis = await _context.ProcedimientosAnalisis.FindAsync(id);
            if (procedimientoAnalisis != null)
            {
                _context.ProcedimientosAnalisis.Remove(procedimientoAnalisis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcedimientoAnalisisExists(int id)
        {
            return _context.ProcedimientosAnalisis.Any(e => e.Id == id);
        }
    }
}
