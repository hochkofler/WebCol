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
    public class FasesMovilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FasesMovilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FasesMoviles
        public async Task<IActionResult> Index()
        {
            return View(await _context.FasesMoviles.ToListAsync());
        }

        // GET: FasesMoviles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faseMovil = await _context.FasesMoviles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faseMovil == null)
            {
                return NotFound();
            }

            return View(faseMovil);
        }

        // GET: FasesMoviles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FasesMoviles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] FaseMovil faseMovil)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faseMovil);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(faseMovil);
        }

        // GET: FasesMoviles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faseMovil = await _context.FasesMoviles.FindAsync(id);
            if (faseMovil == null)
            {
                return NotFound();
            }
            return View(faseMovil);
        }

        // POST: FasesMoviles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] FaseMovil faseMovil)
        {
            if (id != faseMovil.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faseMovil);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaseMovilExists(faseMovil.Id))
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
            return View(faseMovil);
        }

        // GET: FasesMoviles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faseMovil = await _context.FasesMoviles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (faseMovil == null)
            {
                return NotFound();
            }

            return View(faseMovil);
        }

        // POST: FasesMoviles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faseMovil = await _context.FasesMoviles.FindAsync(id);
            if (faseMovil != null)
            {
                _context.FasesMoviles.Remove(faseMovil);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaseMovilExists(int id)
        {
            return _context.FasesMoviles.Any(e => e.Id == id);
        }
    }
}
