using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
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

            var applicationDbContext = _context.Analisis.Include(a => a.Columnas).Include(a => a.Lote)
                .Include(a => a.ProductoPrincipios).ThenInclude(a => a.Principio);
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

            // Configurar SelectList para los lotes
            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id");

            // Si no se proporcionó un ID, simplemente devolver la vista con el modelo vacío
            if (id == null)
            {
                return View(viewModel);
            }

            // Buscar el lote correspondiente
            var lote = await _context.Lotes.FindAsync(id);
            if (lote == null)
            {
                ViewBag.Error = "El lote no existe";
                return Content("<h1>El lote no existe</h1>", "text/html");
            }

            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id", lote.Id);

            // Configurar el lote en el modelo
            viewModel.Lote = lote;
            ViewBag.producto = _context.Productos.FirstOrDefault(p => p.Id == lote.ProductoId);

            // Buscar los principios correspondientes al lote
            viewModel.Principios = await _context.ProductosPrincipios.Include(pp => pp.Principio)
                                        .Where(pp => pp.ProductoId == lote.ProductoId)
                                        .Select(pp => pp.Principio).ToListAsync();

            // Si no hay principios, devolver un error
            if (viewModel.Principios.Count == 0)
            {
                ViewBag.Error = "El lote no tiene principios asociados";
                return Content(ViewBag.Error, "text/html");
            }

            // Configurar SelectList para los principios
            ViewBag.principios = new MultiSelectList(viewModel.Principios, "Id", "Nombre");

            // Si no se proporcionaron principios, simplemente devolver la vista con el modelo

            if (principios.Count == 0 && viewModel.Principios.Count != 1)
            {
                return View(viewModel);
            }

            // Configurar el análisis en el modelo
            viewModel.Analisis = new Analisis
            {
                FechaInicio = DateTime.Now,
                FechaFinal = DateTime.Now,
                LoteId = id
            };

            // Si se proporcionaron principios, configurar los ProductoPrincipios en el análisis
            if (principios != null && principios.Count > 0)
            {
                viewModel.Analisis.ProductoPrincipios = new List<ProductoPrincipio>();

                foreach (var principio in principios)
                {
                    var productoPrincipio = await _context.ProductosPrincipios
                        .FirstOrDefaultAsync(pp => pp.ProductoId == viewModel.Lote.ProductoId && pp.PrincipioId == principio);
                    if (productoPrincipio != null)
                    {
                        viewModel.Analisis.ProductoPrincipios.Add(productoPrincipio);
                    }
                    // devuelve error si no se encuentra el productoPrincipio
                    else
                    {
                        ViewBag.Error = "No se encontró el productoPrincipio";
                        return Content(ViewBag.Error, "text/html");
                    }
                }
            }

            if (viewModel.Principios.Count == 1)
            {
                viewModel.Analisis.ProductoPrincipios =
                [
                    new ProductoPrincipio
                    {
                        ProductoId = viewModel.Lote.ProductoId,
                        PrincipioId = viewModel.Principios[0].Id
                    },
                ];
            }

            // Buscar las columnas correspondientes a los principios

            var asignacionesColumnas = await _context.AsignacionesColumnas.ToListAsync();

            var asignacionesFiltradas = asignacionesColumnas
                .Where(ac => viewModel.Analisis.ProductoPrincipios.Any(pp => pp.ProductoId == ac.ProductoId && pp.PrincipioId == ac.PrincipioId))
                .ToList();

            // Si no hay columnas, devolver un error
            if (asignacionesFiltradas.Count == 0)
            {
                ViewBag.Error = "No hay columnas disponibles para los principios seleccionados";
                return Content(ViewBag.Error, "text/html");
            }

            // Configurar las columnas en el modelo
            viewModel.Columnas = asignacionesFiltradas.Select(ac => ac.Columna).ToList();

            // Configurar SelectList para las columnas
            ViewBag.columnas = new SelectList(asignacionesFiltradas, "ColumnaId", "ColumnaId");
            ViewBag.principios = new MultiSelectList(viewModel.Principios, "Id", "Nombre", viewModel.Analisis.PrincipiosIds);


            // Devolver la vista con el modelo
            return View(viewModel);
        }

        // POST: Analisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColumnaId,FechaInicio,FechaFinal,CategoriaOrigen," +
        "LoteId,PrincipiosIds,Ph,TiempoCorrida,Flujo,Temperatura," +
        "PresionIni,PresionFin,PlatosIni,PlatosFin,Comportamiento,Comentario,")] Analisis analisis)
        {
            // Buscar los ProductoPrincipio correspondientes a los IDs 
            var lote = await _context.Lotes.FindAsync(analisis.LoteId);
            if (lote == null)
            {
                ViewBag.Error = "El lote no existe";
                return Content("<h1>El lote no existe</h1>", "text/html");
            }

            ViewBag.producto = _context.Productos.FirstOrDefault(p => p.Id == lote.ProductoId);

            if (analisis.PrincipiosIds != null && analisis.PrincipiosIds.Count > 0 && lote != null)
            {
                analisis.ProductoPrincipios = new List<ProductoPrincipio>();

                foreach (var principio in analisis.PrincipiosIds)
                {
                    var productoPrincipio = await _context.ProductosPrincipios
                        .FirstOrDefaultAsync(pp => pp.ProductoId == lote.ProductoId && pp.PrincipioId == principio);
                    if (productoPrincipio != null)
                    {
                        analisis.ProductoPrincipios.Add(productoPrincipio);
                    }
                    // devuelve error si no se encuentra el productoPrincipio
                    else
                    {
                        ViewBag.Error = "No se encontró el productoPrincipio";
                        return Content(ViewBag.Error, "text/html");
                    }
                }
            }
            if (ModelState.IsValid)
            { 
                _context.Add(analisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id", analisis.LoteId);
            ViewBag.columnas = new SelectList(_context.Columnas, "Id", "Id", analisis.ColumnaId);
            var viewModel = new AnalisisViewModel();
            viewModel.Analisis = analisis;
            viewModel.Columnas = await _context.Columnas.ToListAsync();


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
            AnalisisViewModel viewModel = new AnalisisViewModel();
            viewModel.Analisis = analisis;
            return View(viewModel);
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
