using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasterlistOfBusiness.Data;
using MasterlistOfBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using System.Drawing.Printing;

namespace MasterlistOfBusiness.Controllers
{
    [Authorize]
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
            var userLogin = User.Identity?.Name;

             if (string.IsNullOrEmpty(userLogin))
                return RedirectToAction("Login", "Account");

            var prac = _context.Konto.Include(p => p.Sprzedawca).Where(p => p.Sprzedawca.UzytkownikLogin == userLogin).AsNoTracking();
            // Można dodać sortowanie, filtrowanie lub paginację, jeśli potrzebne
            return View(await prac.ToListAsync());
        }

        // GET: Konto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Konto == null)
            {
                return NotFound();
            }

            var konto = await _context.Konto.Include(k => k.Inwentarze)
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
            var userLogin = User.Identity.Name;
            var sprzed = _context.Sprzedawca.Where(s => s.UzytkownikLogin == userLogin).ToList();
            ViewData["Sprzedawca"] = new SelectList(sprzed, "id_sprzedawcy", "login");
            return View();
        }

        // POST: Konto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_konta,id_sprzedawcy,link,NazwaUzytkownika,Platforma")] Konto konto)
        {

            var selectedVendor = _context.Sprzedawca.Find(konto.id_sprzedawcy);

            if (selectedVendor == null)
            {
                ModelState.AddModelError("", "Nie wybrano sprzedawcy");
            }

            konto.Sprzedawca = selectedVendor;
            ModelState.Remove("Sprzedawca");

            if (ModelState.IsValid)
            {

                _context.Add(konto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        Console.WriteLine($"Error in {modelState.Key}: {error.ErrorMessage}");
                    }
                }
            }

            return View(konto);
        }

        // GET: Konto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userLogin = User.Identity.Name;
            var sprzed = _context.Sprzedawca.Where(s => s.UzytkownikLogin == userLogin).ToList();
            ViewData["Sprzedawca"] = new SelectList(sprzed, "id_sprzedawcy", "login");

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
