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
    public class AsignacionColumnasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AsignacionColumnasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AsignacionColumnas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AsignacionesColumnas.Include(a => a.Columnas).Include(a => a.ProcedimientoAnalisis);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AsignacionColumnas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionColumna = await _context.AsignacionesColumnas
                .Include(a => a.Columnas)
                .Include(a => a.ProcedimientoAnalisis)
                .FirstOrDefaultAsync(m => m.AsignacionColumnaId == id);
            if (asignacionColumna == null)
            {
                return NotFound();
            }

            return View(asignacionColumna);
        }

        // GET: AsignacionColumnas/Create
        public IActionResult Create()
        {
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id");
            ViewData["ProcedimientoAnalisisId"] = new SelectList(_context.ProcedimientosAnalisis, "Id", "Id");
            return View();
        }

        // POST: AsignacionColumnas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignacionColumnaId,Activa,ColumnaId,ProcedimientoAnalisisId")] AsignacionColumna asignacionColumna)
        {
            if (ModelState.IsValid)
            {
                _context.Add(asignacionColumna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", asignacionColumna.ColumnaId);
            ViewData["ProcedimientoAnalisisId"] = new SelectList(_context.ProcedimientosAnalisis, "Id", "Id", asignacionColumna.ProcedimientoAnalisisId);
            return View(asignacionColumna);
        }

        // GET: AsignacionColumnas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionColumna = await _context.AsignacionesColumnas.FindAsync(id);
            if (asignacionColumna == null)
            {
                return NotFound();
            }
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", asignacionColumna.ColumnaId);
            ViewData["ProcedimientoAnalisisId"] = new SelectList(_context.ProcedimientosAnalisis, "Id", "Id", asignacionColumna.ProcedimientoAnalisisId);
            return View(asignacionColumna);
        }

        // POST: AsignacionColumnas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsignacionColumnaId,Activa,ColumnaId,ProcedimientoAnalisisId")] AsignacionColumna asignacionColumna)
        {
            if (id != asignacionColumna.AsignacionColumnaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacionColumna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionColumnaExists(asignacionColumna.AsignacionColumnaId))
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
            ViewData["ColumnaId"] = new SelectList(_context.Columnas, "Id", "Id", asignacionColumna.ColumnaId);
            ViewData["ProcedimientoAnalisisId"] = new SelectList(_context.ProcedimientosAnalisis, "Id", "Id", asignacionColumna.ProcedimientoAnalisisId);
            return View(asignacionColumna);
        }

        // GET: AsignacionColumnas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionColumna = await _context.AsignacionesColumnas
                .Include(a => a.Columnas)
                .Include(a => a.ProcedimientoAnalisis)
                .FirstOrDefaultAsync(m => m.AsignacionColumnaId == id);
            if (asignacionColumna == null)
            {
                return NotFound();
            }

            return View(asignacionColumna);
        }

        // POST: AsignacionColumnas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacionColumna = await _context.AsignacionesColumnas.FindAsync(id);
            if (asignacionColumna != null)
            {
                _context.AsignacionesColumnas.Remove(asignacionColumna);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionColumnaExists(int id)
        {
            return _context.AsignacionesColumnas.Any(e => e.AsignacionColumnaId == id);
        }
    }
}
