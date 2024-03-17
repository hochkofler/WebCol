using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCol.Data;
using WebCol.Models;
using WebCol.Models.ViewModels;

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

        public async Task<IActionResult> Create(string? id, List<int>? principios)
        {
            var viewModel = new AnalisisViewModel();
            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id");

            if (id == null)
            {
                return View(viewModel);
            }

            var lote = await _context.Lotes.FindAsync(id);
            if(lote == null)
            {
                ViewBag.Error = "El lote no existe";
                return Content("<h1>El lote no existe</h1>", "text/html");
                //return View(viewModel);
            }

            viewModel.Lote = lote;
            
            viewModel.Principios = await _context.ProductosPrincipios.Include(pp => pp.Principio)
                                        .Where(pp => pp.ProductoId == lote.ProductoId)
                                        .Select(pp => pp.Principio).ToListAsync();

            if (viewModel.Principios.Count == 0)
            {
                ViewBag.Error = "El lote no tiene principios asociados";
                return Content(ViewBag.Error, "text/html");
                //return View(viewModel);
            }
            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id", id);
            ViewBag.principios = new MultiSelectList(viewModel.Principios, "Id", "Nombre");

            if (principios.Count == 0)
            {
                return View(viewModel);
            }

            // Validar que todos los principios estén en el productoPrincipiosValidos

            foreach (var principio in principios)
            {
                if(!viewModel.Principios.Any(pp => pp.Id == principio))
                {
                    ViewBag.Error = "El/los principio(s) no pertenece al producto";
                    return NotFound();
                }
            }

            var productoId = lote.ProductoId; // Obtén el ProductoId del lote
            var principiosIds = principios; // Obtén la lista de PrincipiosIds

            var productosPrincipios = await _context.ProductosPrincipios
                .Where(pp => pp.ProductoId == productoId && principiosIds.Contains(pp.PrincipioId))
                .ToListAsync();

            var asignacionesColumnas = await _context.AsignacionesColumnas.ToListAsync();

            var asignacionesFiltradas = asignacionesColumnas
                .Where(ac => productosPrincipios.Any(pp => pp.ProductoId == ac.ProductoId && pp.PrincipioId == ac.PrincipioId))
                .ToList();

            if (asignacionesFiltradas.Count == 0)
            {
                ViewBag.Error = "No hay columnas disponibles para los principios seleccionados";
                return Content(ViewBag.Error, "text/html");
            }

            viewModel.Columnas = asignacionesFiltradas.Select(ac => ac.Columna).ToList();

            if (viewModel.Columnas.Count == 0)
            {
                ViewBag.Error = "No hay columnas disponibles para los principios seleccionados2";
                return Content(ViewBag.Error, "text/html");
            }

            ViewBag.columnas = new SelectList(asignacionesFiltradas, "ColumnaId", "ColumnaId");
            
            return View(viewModel);

        }

        // POST: Analisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColumnaId,FechaInicio,FechaFinal,CategoriaOrigen,LoteId,PrincipiosActivosId,Ph,TiempoCorrida,Flujo,Temperatura,PresionIni,PresionFin,PlatosIni,PlatosFin,Comportamiento,Comentario")] Analisis analisis)
        {


            var analisis1 = new Analisis
            {
                Ph = analisis.Ph,
                TiempoCorrida = analisis.TiempoCorrida,
                Flujo = analisis.Flujo,
                Temperatura = analisis.Temperatura,
                PresionIni = analisis.PresionIni,
                PresionFin = analisis.PresionFin,
                Comentario = analisis.Comentario,
                LoteId = analisis.LoteId,
                ColumnaId = analisis.ColumnaId,
                PrincipiosActivosId = analisis.PrincipiosActivosId
            };

            _context.Add(analisis1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            if (ModelState.IsValid)
            {
                _context.Add(analisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColumnaId"] = new SelectList(_context.Set<Columna>(), "Id", "Id", analisis.ColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Set<Lote>(), "Id", "Id", analisis.LoteId);

            // Crear un nuevo AnalisisViewModel y asignarle los valores de 'analisis'
            var viewModel = new AnalisisViewModel
            {
                Analisis = analisis,
                // Asegúrate de asignar todas las propiedades necesarias
            };

            return View(viewModel);
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
    }
}
