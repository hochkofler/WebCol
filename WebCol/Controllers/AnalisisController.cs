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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        public async Task<IActionResult> Create(string? id, int? procedimientoIdElegido)
        {
            var viewModel = new AnalisisViewModel();

            // Configurar SelectList para los selección de lote
            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id");

            // Si no se proporcionó un ID, simplemente devolver la vista con la selección de lote
            if (id == null)
            {
                return View(viewModel);
            }

            // Buscar el lote correspondiente ------------no funciona
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

            // Buscar todos los procedimientos que corresponden al lote
            var procedimientos = await _context.ProcedimientosAnalisis
                .Include(pa => pa.Producto)
                .Include(pa => pa.Principio)
                .Where(pa => pa.ProductoId == lote.ProductoId)
                .ToListAsync();

            // Si no hay procedimientos, devolver un error
            if (procedimientos.Count == 0)
            {
                ViewBag.Error = "El producto no tiene procedimientos de análisis asociados";
                return Content(ViewBag.Error, "text/html");
            }

            viewModel.Procedimientos = procedimientos;
            // Configurar SelectList para los procedimientos para segunda vista
            var procedimientosFormateados = procedimientos.Select(pa => new {
                pa.Id,
                Descripcion = $"{pa.Principio.Id} - {pa.Principio.Nombre}"
            }).ToList();

            ViewBag.procedimientos = new SelectList(procedimientosFormateados, "Id", "Descripcion");

            // Si no se proporcionaron procedimientos, simplemente devolver la vista con el modelo
            if (procedimientoIdElegido == null && procedimientos.Count != 1)
            {
                return View(viewModel);
            }

            procedimientoIdElegido ??= procedimientos[0].Id;
            // ---------- Tercera parte del método ----------
            // Configurar el análisis en el modelo
            viewModel.Analisis = new Analisis
            {
                FechaInicio = DateTime.Now,
                FechaFinal = DateTime.Now,
                LoteId = id
            };

            // validar que procedimientoIdElegido corresponda al producto y no sea nulo
            if (procedimientoIdElegido != null && procedimientos.Count > 0)
            {
                if (procedimientos.FirstOrDefault(p => p.Id == procedimientoIdElegido).ProductoId != lote.ProductoId)
                {
                    ViewBag.Error = "El procedimiento no corresponde al producto";
                    return Content(ViewBag.Error, "text/html");
                }
            }            
            // Configurar todas las asignaciones de columnas en el modelo que corresponden al ProcedimientoAnalisis
            var asignacionesColumnas = await _context.AsignacionesColumnas
                .Include(ac => ac.Columnas)
                .Where(ac => ac.ProcedimientoAnalisisId == procedimientoIdElegido).ToListAsync();

            // Si no hay asignaciones, devolver un error
            if (asignacionesColumnas.Count == 0)
            {
                ViewBag.Error = "No hay asignaciones de columnas disponibles para el procedimiento seleccionado";
                return Content(ViewBag.Error, "text/html");
            }

            // Configurar las asignaciones de columnas en el modelo
            viewModel.AsignacionColumnas = asignacionesColumnas;
            ViewBag.asignacionesColumnas = new SelectList(asignacionesColumnas, "AsignacionColumnaId", "AsignacionColumnaId");
            ViewBag.procedimientos = new SelectList(procedimientosFormateados, "Id", "Descripcion",procedimientoIdElegido);

            
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
                analisis.Usuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(analisis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.lotes = new SelectList(_context.Lotes, "Id", "Id", analisis.LoteId);
            ViewBag.columnas = new SelectList(_context.Columnas, "Id", "Id", analisis.AsignacionColumnaId);
            var viewModel = new AnalisisViewModel();
            viewModel.Analisis = analisis;
           
            // configurar columnas correspondientes al procedimiento del analisis
            if (analisis.AsignacionColumnas != null)
            {
                var asignacionColumna = await _context.AsignacionesColumnas
                .Include(ac => ac.Columnas)
                .Where(ac => ac.ProcedimientoAnalisisId == analisis.AsignacionColumnas.ProcedimientoAnalisisId).ToListAsync();

                if (asignacionColumna != null)
                {
                    viewModel.AsignacionColumnas = asignacionColumna;
                }
            }

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
