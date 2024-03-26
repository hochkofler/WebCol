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
    public class LavadosRegeneracionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LavadosRegeneracionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LavadosRegeneraciones
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LavadosRegeneraciones.Include(l => l.Analisis).Include(l => l.Columna);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LavadosRegeneraciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavadoRegeneracion = await _context.LavadosRegeneraciones
                .Include(l => l.Analisis)
                .Include(l => l.Columna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lavadoRegeneracion == null)
            {
                return NotFound();
            }

            return View(lavadoRegeneracion);
        }

        // GET: LavadosRegeneraciones/Create
        public IActionResult Create()
        {
            ViewData["AnalisisId"] = new SelectList(_context.Analisis, "Id", "Id");
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id");
            return View();
        }

        // POST: LavadosRegeneraciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,ColumnaId,AnalisisId")] LavadoRegeneracion lavadoRegeneracion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lavadoRegeneracion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnalisisId"] = new SelectList(_context.Analisis, "Id", "Id", lavadoRegeneracion.AnalisisId);
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", lavadoRegeneracion.ColumnaId);
            return View(lavadoRegeneracion);
        }

        // GET: LavadosRegeneraciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavadoRegeneracion = await _context.LavadosRegeneraciones.FindAsync(id);
            if (lavadoRegeneracion == null)
            {
                return NotFound();
            }
            ViewData["AnalisisId"] = new SelectList(_context.Analisis, "Id", "Id", lavadoRegeneracion.AnalisisId);
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", lavadoRegeneracion.ColumnaId);
            return View(lavadoRegeneracion);
        }

        // POST: LavadosRegeneraciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,ColumnaId,AnalisisId")] LavadoRegeneracion lavadoRegeneracion)
        {
            if (id != lavadoRegeneracion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lavadoRegeneracion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LavadoRegeneracionExists(lavadoRegeneracion.Id))
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
            ViewData["AnalisisId"] = new SelectList(_context.Analisis, "Id", "Id", lavadoRegeneracion.AnalisisId);
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", lavadoRegeneracion.ColumnaId);
            return View(lavadoRegeneracion);
        }

        // GET: LavadosRegeneraciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lavadoRegeneracion = await _context.LavadosRegeneraciones
                .Include(l => l.Analisis)
                .Include(l => l.Columna)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lavadoRegeneracion == null)
            {
                return NotFound();
            }

            return View(lavadoRegeneracion);
        }

        // POST: LavadosRegeneraciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lavadoRegeneracion = await _context.LavadosRegeneraciones.FindAsync(id);
            if (lavadoRegeneracion != null)
            {
                _context.LavadosRegeneraciones.Remove(lavadoRegeneracion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LavadoRegeneracionExists(int id)
        {
            return _context.LavadosRegeneraciones.Any(e => e.Id == id);
        }
    }
}
