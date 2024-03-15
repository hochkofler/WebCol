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
    public class AnalisisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnalisisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Analisis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Analisis.Include(a => a.Columnas).Include(a => a.Lote);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Analisis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analisis = await _context.Analisis
                .Include(a => a.Columnas)
                .Include(a => a.Lote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analisis == null)
            {
                return NotFound();
            }

            return View(analisis);
        }

        // GET: Analisis/Create
        public IActionResult Create()
        {
            ViewData["ColumnaId"] = new SelectList(_context.Set<Columna>(), "Id", "Id");
            ViewData["LoteId"] = new SelectList(_context.Set<Lote>(), "Id", "Id");
            return View();
        }

        // POST: Analisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColumnaId,FechaInicio,FechaFinal,CategoriaOrigen,LoteId,Ph,TiempoCorrida,Flujo,Temperatura,PresionIni,PresionFin,PlatosIni,PlatosFin,Comportamiento,Comentario")] Analisis analisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnaId"] = new SelectList(_context.Set<Columna>(), "Id", "Id", analisis.ColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Set<Lote>(), "Id", "Id", analisis.LoteId);
            return View(analisis);
        }

        // GET: Analisis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analisis = await _context.Analisis.FindAsync(id);
            if (analisis == null)
            {
                return NotFound();
            }
            ViewData["ColumnaId"] = new SelectList(_context.Set<Columna>(), "Id", "Id", analisis.ColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Set<Lote>(), "Id", "Id", analisis.LoteId);
            return View(analisis);
        }

        // POST: Analisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColumnaId,FechaInicio,FechaFinal,CategoriaOrigen,LoteId,Ph,TiempoCorrida,Flujo,Temperatura,PresionIni,PresionFin,PlatosIni,PlatosFin,Comportamiento,Comentario")] Analisis analisis)
        {
            if (id != analisis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analisis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalisisExists(analisis.Id))
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
            ViewData["ColumnaId"] = new SelectList(_context.Set<Columna>(), "Id", "Id", analisis.ColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Set<Lote>(), "Id", "Id", analisis.LoteId);
            return View(analisis);
        }

        // GET: Analisis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analisis = await _context.Analisis
                .Include(a => a.Columnas)
                .Include(a => a.Lote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analisis == null)
            {
                return NotFound();
            }

            return View(analisis);
        }

        // POST: Analisis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analisis = await _context.Analisis.FindAsync(id);
            if (analisis != null)
            {
                _context.Analisis.Remove(analisis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalisisExists(int id)
        {
            return _context.Analisis.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Header()
        {
            var lotes = await _context.Lotes.ToListAsync();
            return View(lotes);
        }

        // POST: Analisis/Header
        [HttpPost]
        public async Task<IActionResult> Header(string idLote)
        {
            return RedirectToAction("Create", "Analisis", new { id = idLote });
        }

        public async Task<IActionResult> Create(string id)
        {
            var lote = await _context.Lotes.FindAsync(id);
            if (lote != null)
            {
                return NotFound();
            }
            var analisis = new Analisis { LoteId = id };
            ViewData["Principios"] = new MultiSelectList(ObtenerPrincipiosPorLote(id), "Id", "Nombre");
            return View(analisis);
        }

        // Obtener principios por lote
        private List<Principio> ObtenerPrincipiosPorLote(string id)
        {
            var lote = _context.Lotes.Find(id);

            if (lote == null)
            {
                return new List<Principio>();
                //return Json(new List<PrincipiosActivos>());
            }
            return _context.Principios.Include(p => p.ProductosPrincipios).ThenInclude(pp => pp.Producto).Where(p => p.Id == lote.ProductoId).ToList();
        }
    }
}
