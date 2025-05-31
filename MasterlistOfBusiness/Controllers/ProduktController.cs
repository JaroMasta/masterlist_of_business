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

namespace MasterlistOfBusiness.Controllers
{
    [Authorize]
    public class ProduktController : Controller
    {
        private readonly MOBContext _context;

        public ProduktController(MOBContext context)
        {
            _context = context;
        }

        // GET: Produkt 
        public async Task<IActionResult> Index()
        {
            var userLogin = User.Identity?.Name;

            if (string.IsNullOrEmpty(userLogin))
                return RedirectToAction("Login", "Account");

            var produkty = _context.Produkt.Include(p => p.Konto).
                Where(p => p.Konto.Sprzedawca.UzytkownikLogin == userLogin)
                .AsNoTracking();
            // Można dodać sortowanie, filtrowanie lub paginację, jeśli potrzebne
            return View(await produkty.ToListAsync());
        }

        // GET: Produkt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.id_produktu == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // GET: Produkt/Create
        public IActionResult Create()
        {
            var userLogin = User.Identity?.Name;
            var konto = _context.Konto.Where(s => s.Sprzedawca.UzytkownikLogin == userLogin).ToList();
            ViewData["Konto"] = new SelectList(konto, "id_konta", "NazwaUzytkownika");
            return View();
        }

        // POST: Produkt/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_produktu,typ,opis,cena,ilosc,id_konta")] Produkt produkt)
        {
            var selectedAccount = _context.Konto.Find(produkt.id_konta);
            if (selectedAccount == null)
            {
                ModelState.AddModelError("", "Nie wybrano konta");
            }

            produkt.Konto = selectedAccount;
            ModelState.Remove("Konto");

            if (ModelState.IsValid)
            {
                _context.Add(produkt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState)
                {
                    foreach (var error in modelState.Value.Errors)
                    {
                        ModelState.AddModelError("", error.ErrorMessage);
                    }
                }
            }
            return View(produkt);
        }

        // GET: Produkt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var userLogin = User.Identity?.Name;
            var konto = _context.Konto.Where(s => s.Sprzedawca.UzytkownikLogin == userLogin).ToList();
            ViewData["Konto"] = new SelectList(konto, "id_konta", "NazwaUzytkownika");
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt.FindAsync(id);
            if (produkt == null)
            {
                return NotFound();
            }
            return View(produkt);
        }

        // POST: Produkt/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_produktu,typ,opis,cena,ilosc,id_konta")] Produkt produkt)
        {
            var selectedAccount = _context.Konto.Find(produkt.id_konta);

            if (selectedAccount == null)
            {
                ModelState.AddModelError("", "Nie wybrano sprzedawcy");
            }

            produkt.Konto = selectedAccount;
            ModelState.Remove("Konto");
            
            if (id != produkt.id_produktu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produkt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduktExists(produkt.id_produktu))
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
            return View(produkt);
        }

        // GET: Produkt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Produkt == null)
            {
                return NotFound();
            }

            var produkt = await _context.Produkt
                .FirstOrDefaultAsync(m => m.id_produktu == id);
            if (produkt == null)
            {
                return NotFound();
            }

            return View(produkt);
        }

        // POST: Produkt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Produkt == null)
            {
                return Problem("Entity set 'MOBContext.Produkt'  is null.");
            }
            var produkt = await _context.Produkt.FindAsync(id);
            if (produkt != null)
            {
                _context.Produkt.Remove(produkt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProduktExists(int id)
        {
            return (_context.Produkt?.Any(e => e.id_produktu == id)).GetValueOrDefault();
        }
    }
}
