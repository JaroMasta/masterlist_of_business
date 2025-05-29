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
    public class SprzedawcaController : Controller
    {
        private readonly MOBContext _context;

        public SprzedawcaController(MOBContext context)
        {
            _context = context;
        }

        // GET: Sprzedawca
        public async Task<IActionResult> Index()
        {
            var sprzed = _context.Sprzedawca.Include(p => p.Konta).Include(p => p.Uzytkownik).AsNoTracking();
              return _context.Sprzedawca != null ?
                        View(await sprzed.ToListAsync()) :
                        Problem("Entity set 'MOBContext.Sprzedawca'  is null.");
        }

        // GET: Sprzedawca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sprzedawca == null)
            {
                return NotFound();
            }

            var sprzedawca = await _context.Sprzedawca.Include(p => p.Konta).Include(p => p.Uzytkownik)
                .FirstOrDefaultAsync(m => m.id_sprzedawcy == id);
            if (sprzedawca == null)
            {
                return NotFound();
            }

            return View(sprzedawca);
        }

        private void PopulateKontaDropDownList(object selectedKonto = null)
        {
            var Konta = from e in _context.Konto
                                orderby e.NazwaUzytkownika
                                select e;
            var res = Konta.AsNoTracking();
            ViewBag.KontaID = new SelectList(res, "Id", "Nazwa", selectedKonto);
        }

        private void PopulateUzytkownikDropDownList(object selectedUzytkownik = null)
        {
            var Uzytkownik = from e in _context.Uzytkownik
                                orderby e.login
                                select e;
            var res = Uzytkownik.AsNoTracking();
            ViewBag.UzytkownikID = new SelectList(res, "Id", "Nazwa", selectedUzytkownik);
        }

        // GET: Sprzedawca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sprzedawca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_sprzedawcy,login")] Sprzedawca sprzedawca, IFormCollection form)
        {
            string kontoValue = form["Konto"].ToString();
            string uzytkownikValue = form["Uzytkownik"].ToString();
            if (ModelState.IsValid)
            {
                Konto konto = null;
                if (kontoValue != "-1")
                {
                    var k = _context.Konto.Where(k => k.id_konta == int.Parse(kontoValue));
                    if (k.Count() > 0)
                        konto = k.First();
                }

                Uzytkownik uzytkownik = null;
                if (uzytkownikValue != "-1")
                {
                    var k = _context.Uzytkownik.Where(k => k.login == uzytkownikValue);
                    if (k.Count() > 0)
                        uzytkownik = k.First();
                }

                _context.Add(sprzedawca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sprzedawca);
        }

        // GET: Sprzedawca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sprzedawca == null)
            {
                return NotFound();
            }

            var sprzedawca = await _context.Sprzedawca.FindAsync(id);
            if (sprzedawca == null)
            {
                return NotFound();
            }
            return View(sprzedawca);
        }

        // POST: Sprzedawca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_sprzedawcy,login")] Sprzedawca sprzedawca)
        {
            if (id != sprzedawca.id_sprzedawcy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sprzedawca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SprzedawcaExists(sprzedawca.id_sprzedawcy))
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
            return View(sprzedawca);
        }

        // GET: Sprzedawca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sprzedawca == null)
            {
                return NotFound();
            }

            var sprzedawca = await _context.Sprzedawca
                .FirstOrDefaultAsync(m => m.id_sprzedawcy == id);
            if (sprzedawca == null)
            {
                return NotFound();
            }

            return View(sprzedawca);
        }

        // POST: Sprzedawca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sprzedawca == null)
            {
                return Problem("Entity set 'MOBContext.Sprzedawca'  is null.");
            }
            var sprzedawca = await _context.Sprzedawca.FindAsync(id);
            if (sprzedawca != null)
            {
                _context.Sprzedawca.Remove(sprzedawca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SprzedawcaExists(int id)
        {
          return (_context.Sprzedawca?.Any(e => e.id_sprzedawcy == id)).GetValueOrDefault();
        }
    }
}
