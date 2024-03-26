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
    public class ColumnasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ColumnasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Columnas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Columnas.Include(c => c.Modelo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Columnas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columnas
                .Include(c => c.Modelo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (columna == null)
            {
                return NotFound();
            }

            return View(columna);
        }

        // GET: Columnas/Create
        public IActionResult Create()
        {
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Id");
            return View();
        }

        // POST: Columnas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FechaIngreso,FechaEnMarcha,Dimension,FaseEstacionaria,Clase,PhMin,PhMax,PresionMax,ModeloId")] Columna columna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(columna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Id", columna.ModeloId);
            return View(columna);
        }

        // GET: Columnas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columnas.FindAsync(id);
            if (columna == null)
            {
                return NotFound();
            }
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Id", columna.ModeloId);
            return View(columna);
        }

        // POST: Columnas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,FechaIngreso,FechaEnMarcha,Dimension,FaseEstacionaria,Clase,PhMin,PhMax,PresionMax,ModeloId")] Columna columna)
        {
            if (id != columna.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(columna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColumnaExists(columna.Id))
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
            ViewData["ModeloId"] = new SelectList(_context.Modelos, "Id", "Id", columna.ModeloId);
            return View(columna);
        }

        // GET: Columnas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var columna = await _context.Columnas
                .Include(c => c.Modelo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (columna == null)
            {
                return NotFound();
            }

            return View(columna);
        }

        // POST: Columnas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var columna = await _context.Columnas.FindAsync(id);
            if (columna != null)
            {
                _context.Columnas.Remove(columna);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColumnaExists(string id)
        {
            return _context.Columnas.Any(e => e.Id == id);
        }
    }
}
