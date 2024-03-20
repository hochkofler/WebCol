using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using WebCol.Data;
using WebCol.Models;
using WebCol.Models.ViewModels;
using System.Security.Claims;

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
            var applicationDbContext = _context.Analisis.Include(a => a.AsignacionColumnas).Include(a => a.Lote);
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
                .Include(a => a.AsignacionColumnas)
                .Include(a => a.Lote)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (analisis == null)
            {
                return NotFound();
            }

            return View(analisis);
        }

        //// GET: Analisis/Create
        //public IActionResult Create()
        //{
        //    ViewData["AsignacionColumnaId"] = new SelectList(_context.AsignacionesColumnas, "AsignacionColumnaId", "AsignacionColumnaId");
        //    ViewData["LoteId"] = new SelectList(_context.Lotes, "Id", "Id");
        //    return View();
        //}
        public async Task<IActionResult> Create(string? id, List<int>? procedimientosIdsElegidos)
        {
            var viewModel = new AnalisisViewModel();

            // Configurar SelectList para los selección de lote
            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id");

            // Si no se proporcionó un ID, simplemente devolver la vista con la selección de lote
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

            // ---------- Segunda parte del método ----------
            // Ya tenemos un lote

            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id", lote.Id);

            // Configurar el lote elegido en el modelo y producto
            viewModel.Lote = lote;

            var producto = _context.Productos.FirstOrDefault(p => p.Id == lote.ProductoId);
            ViewBag.producto = producto;

            // Buscar todas los procedimientos de analisis que corresponden al producto
            var procedimientos = await _context.ProcedimientosAnalisis
                .Include(pa => pa.Producto)
                .Include(pa => pa.Principio)
                .Where(pa => pa.ProductoId == lote.ProductoId).ToListAsync();

            // Si no hay procedimientos, devolver un error
            if (procedimientos.Count == 0)
            {
                ViewBag.Error = "El producto no tiene procedimientos de análisis asociados";
                return Content(ViewBag.Error, "text/html");
            }

            // Configurar SelectList para los procedimientos para segunda vista
            var procedimientosFormateados = procedimientos.Select(pa => new {
                Id = pa.Id,
                Descripcion = $"{pa.Principio.Id} - {pa.Principio.Nombre}"
            }).ToList();

            ViewBag.procedimientos = new MultiSelectList(procedimientosFormateados, "Id", "Descripcion");

            // Si no se proporcionaron principios, simplemente devolver la vista con el modelo

            if (procedimientosIdsElegidos.Count == 0 && procedimientos.Count != 1)
            {
                return View(viewModel);
            }

            // ---------- Tercera parte del método ----------

            // Configurar el análisis en el modelo
            viewModel.Analisis = new Analisis
            {
                FechaInicio = DateTime.Now,
                FechaFinal = DateTime.Now,
                LoteId = id
            };

            // validar que procedimientosIdsElegidos existan
            var procedimientosIds = procedimientos.Select(p => p.Id).ToList();
            foreach (var procedimientoId in procedimientosIdsElegidos)
            {
                if (!procedimientosIds.Contains(procedimientoId))
                {
                    ViewBag.Error = "El procedimiento no existe";
                    return Content(ViewBag.Error, "text/html");
                }
            }

            // validar que todos los procedimientosIdsElegidos correspondan al producto
            foreach (var procedimientoId in procedimientosIdsElegidos)
            {
                if (procedimientos.FirstOrDefault(p => p.Id == procedimientoId).ProductoId != lote.ProductoId)
                {
                    ViewBag.Error = "El procedimiento no corresponde al producto";
                    return Content(ViewBag.Error, "text/html");
                }
            }

            //// Si se proporcionaron principios, configurar los ProductoPrincipios en el análisis
            //if (principios != null && principios.Count > 0)
            //{
            //    viewModel.Analisis.AsignacionColumnas.ProcedimientoAnalisis.ProductoPrincipios = new List<ProductoPrincipio>();

            //    foreach (var principio in principios)
            //    {
            //        var productoPrincipio = await _context.ProductosPrincipios
            //            .FirstOrDefaultAsync(pp => pp.ProductoId == viewModel.Lote.ProductoId && pp.PrincipioId == principio);
            //        if (productoPrincipio != null)
            //        {
            //            viewModel.Analisis.AsignacionColumnas.ProcedimientoAnalisis.ProductoPrincipios.Add(productoPrincipio);
            //        }
            //        // devuelve error si no se encuentra el productoPrincipio
            //        else
            //        {
            //            ViewBag.Error = "No se encontró el productoPrincipio";
            //            return Content(ViewBag.Error, "text/html");
            //        }
            //    }
            //}

            //if (viewModel.Principios.Count == 1)
            //{
            //    viewModel.Analisis.AsignacionColumnas.ProcedimientoAnalisis.ProductoPrincipios =
            //    [
            //        new ProductoPrincipio
            //        {
            //            ProductoId = viewModel.Lote.ProductoId,
            //            PrincipioId = viewModel.Principios[0].Id
            //        },
            //    ];
            //}

            //// Buscar las columnas correspondientes a los principios

            //var asignacionesColumnas = await _context.AsignacionesColumnas.ToListAsync();

            //var asignacionesFiltradas = asignacionesColumnas
            //    .Where(ac => viewModel.Analisis.AsignacionColumnas.ProcedimientoAnalisis.ProductoPrincipios.Any(pp => pp.ProductoId == ac.ProductoId && pp.PrincipioId == ac.PrincipioId))
            //    .ToList();

            //// Si no hay columnas, devolver un error
            //if (asignacionesFiltradas.Count == 0)
            //{
            //    ViewBag.Error = "No hay columnas disponibles para los principios seleccionados";
            //    return Content(ViewBag.Error, "text/html");
            //}

            //// Configurar las columnas en el modelo
            //viewModel.Columnas = asignacionesFiltradas.Select(ac => ac.Columnas).ToList();

            //// Configurar SelectList para las columnas
            //ViewBag.columnas = new SelectList(asignacionesFiltradas, "ColumnaId", "ColumnaId");
            //ViewBag.principios = new MultiSelectList(viewModel.Principios, "Id", "Nombre", viewModel.Analisis.PrincipiosIds);
            //var userName = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //viewModel.Analisis.Usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);


            // Devolver la vista con el modelo
            return View(viewModel);
        }

        // POST: Analisis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AsignacionColumnaId,FechaInicio,FechaFinal,CategoriaOrigen,LoteId,Ph,TiempoCorrida,Inyecciones,Flujo,Temperatura,PresionIni,PresionFin,PlatosIni,PlatosFin,Comentario,Usuario")] Analisis analisis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignacionColumnaId"] = new SelectList(_context.AsignacionesColumnas, "AsignacionColumnaId", "AsignacionColumnaId", analisis.AsignacionColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Lotes, "Id", "Id", analisis.LoteId);
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
            ViewData["AsignacionColumnaId"] = new SelectList(_context.AsignacionesColumnas, "AsignacionColumnaId", "AsignacionColumnaId", analisis.AsignacionColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Lotes, "Id", "Id", analisis.LoteId);
            return View(analisis);
        }

        // POST: Analisis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AsignacionColumnaId,FechaInicio,FechaFinal,CategoriaOrigen,LoteId,Ph,TiempoCorrida,Inyecciones,Flujo,Temperatura,PresionIni,PresionFin,PlatosIni,PlatosFin,Comentario,Usuario")] Analisis analisis)
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
            ViewData["AsignacionColumnaId"] = new SelectList(_context.AsignacionesColumnas, "AsignacionColumnaId", "AsignacionColumnaId", analisis.AsignacionColumnaId);
            ViewData["LoteId"] = new SelectList(_context.Lotes, "Id", "Id", analisis.LoteId);
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
                .Include(a => a.AsignacionColumnas)
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
