using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasterlistOfBusiness.Data;
using MasterlistOfBusiness.Models;

namespace MasterlistOfBusiness.Controllers
{
    public class KontoController : Controller
    {
        private readonly MOBContext _context;

        public KontoController(MOBContext context)
        {
            _context = context;
        }

        // GET: Konto
        public async Task<IActionResult> Index()
        {
              return _context.Konto != null ? 
                          View(await _context.Konto.ToListAsync()) :
                          Problem("Entity set 'MOBContext.Konto'  is null.");
        }

        // GET: Konto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Konto == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.id_konta == id);
            if (konto == null)
            {
                return NotFound();
            }

            return View(konto);
        }

        // GET: Konto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Konto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_konta,id_sprzedawcy,link,NazwaUzytkownika,Platforma")] Konto konto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(konto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(konto);
        }

        // GET: Konto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Konto == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto.FindAsync(id);
            if (konto == null)
            {
                return NotFound();
            }
            return View(konto);
        }

        // POST: Konto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_konta,id_sprzedawcy,link,NazwaUzytkownika,Platforma")] Konto konto)
        {
            if (id != konto.id_konta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(konto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontoExists(konto.id_konta))
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
            return View(konto);
        }

        // GET: Konto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Konto == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto
                .FirstOrDefaultAsync(m => m.id_konta == id);
            if (konto == null)
            {
                return NotFound();
            }

            return View(konto);
        }

        // POST: Konto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Konto == null)
            {
                return Problem("Entity set 'MOBContext.Konto'  is null.");
            }
            var konto = await _context.Konto.FindAsync(id);
            if (konto != null)
            {
                _context.Konto.Remove(konto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontoExists(int id)
        {
          return (_context.Konto?.Any(e => e.id_konta == id)).GetValueOrDefault();
        }
    }
}
