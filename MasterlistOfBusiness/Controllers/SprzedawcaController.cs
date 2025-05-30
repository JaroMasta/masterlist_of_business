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
            var userLogin = User.Identity.Name;

            if (string.IsNullOrEmpty(userLogin))
            {
                return RedirectToAction("Login", "Account");
            }

            var sprzed = _context.Sprzedawca.Include(s => s.Uzytkownik).Include(s => s.Konta).
                            Where(s => s.UzytkownikLogin == userLogin).AsNoTracking();
            
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
        public async Task<IActionResult> Create(Sprzedawca sprzedawca, IFormCollection form)
        {
            var userLogin = User.Identity.Name;
            sprzedawca.UzytkownikLogin = userLogin;
            ModelState.Remove("UzytkownikLogin");

            if (string.IsNullOrEmpty(userLogin))
                return RedirectToAction("Login", "Account");

            sprzedawca.Uzytkownik = _context.Uzytkownik.FirstOrDefault(u => u.login == userLogin);
            
            if (ModelState.IsValid)
            {
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
