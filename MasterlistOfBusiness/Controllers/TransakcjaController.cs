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
    public class TransakcjaController : Controller
    {
        private readonly MOBContext _context;

        public TransakcjaController(MOBContext context)
        {
            _context = context;
        }

        // GET: Transakcja
        public async Task<IActionResult> Index()
        {
            var tr = _context.Transakcja.Include(t => t.Produkty).AsNoTracking();
              return _context.Transakcja != null ?
                        View(await tr.ToListAsync()) :
                        Problem("Entity set 'MOBContext.Transakcja'  is null.");
        }

        // GET: Transakcja/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Transakcja == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcja.Include(t => t.Produkty)
                .FirstOrDefaultAsync(m => m.id_transakcji == id);
            if (transakcja == null)
            {
                return NotFound();
            }

            return View(transakcja);
        }


        // GET: Transakcja/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Transakcja/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_transakcji,id_konta,id_produktu")] Transakcja transakcja, IFormCollection form)
        {
            string inwentarzValue = form["Inwentarz"].ToString();
            if (ModelState.IsValid)
            {
                Produkt produkt = null;
                if (inwentarzValue != "-1")
                {
                    var k = _context.Produkt.Where(k => k.id_produktu == int.Parse(inwentarzValue));
                    if (k.Count() > 0)
                        produkt = k.First();
                }

                _context.Add(transakcja);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transakcja);
        }

        // GET: Transakcja/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Transakcja == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcja.FindAsync(id);
            if (transakcja == null)
            {
                return NotFound();
            }
            return View(transakcja);
        }

        // POST: Transakcja/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_transakcji,id_konta,id_produktu")] Transakcja transakcja)
        {
            if (id != transakcja.id_transakcji)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transakcja);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransakcjaExists(transakcja.id_transakcji))
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
            return View(transakcja);
        }

        // GET: Transakcja/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transakcja == null)
            {
                return NotFound();
            }

            var transakcja = await _context.Transakcja
                .FirstOrDefaultAsync(m => m.id_transakcji == id);
            if (transakcja == null)
            {
                return NotFound();
            }

            return View(transakcja);
        }

        // POST: Transakcja/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transakcja == null)
            {
                return Problem("Entity set 'MOBContext.Transakcja'  is null.");
            }
            var transakcja = await _context.Transakcja.FindAsync(id);
            if (transakcja != null)
            {
                _context.Transakcja.Remove(transakcja);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransakcjaExists(int id)
        {
            return (_context.Transakcja?.Any(e => e.id_transakcji == id)).GetValueOrDefault();
        }
    }
}
