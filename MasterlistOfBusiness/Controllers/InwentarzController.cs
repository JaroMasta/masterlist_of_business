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
    public class InwentarzController : Controller
    {
        private readonly MOBContext _context;

        public InwentarzController(MOBContext context)
        {
            _context = context;
        }

        // GET: Inwentarz
        public async Task<IActionResult> Index()
        {
              return _context.Inwentarz != null ? 
                          View(await _context.Inwentarz.ToListAsync()) :
                          Problem("Entity set 'MOBContext.Inwentarz'  is null.");
        }

        // GET: Inwentarz/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inwentarz == null)
            {
                return NotFound();
            }

            var inwentarz = await _context.Inwentarz
                .FirstOrDefaultAsync(m => m.id_inwentarza == id);
            if (inwentarz == null)
            {
                return NotFound();
            }

            return View(inwentarz);
        }

        // GET: Inwentarz/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inwentarz/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_inwentarza,id_konta,id_produktu,cena,ilosc")] Inwentarz inwentarz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inwentarz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inwentarz);
        }

        // GET: Inwentarz/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inwentarz == null)
            {
                return NotFound();
            }

            var inwentarz = await _context.Inwentarz.FindAsync(id);
            if (inwentarz == null)
            {
                return NotFound();
            }
            return View(inwentarz);
        }

        // POST: Inwentarz/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_inwentarza,id_konta,id_produktu,cena,ilosc")] Inwentarz inwentarz)
        {
            if (id != inwentarz.id_inwentarza)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inwentarz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InwentarzExists(inwentarz.id_inwentarza))
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
            return View(inwentarz);
        }

        // GET: Inwentarz/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inwentarz == null)
            {
                return NotFound();
            }

            var inwentarz = await _context.Inwentarz
                .FirstOrDefaultAsync(m => m.id_inwentarza == id);
            if (inwentarz == null)
            {
                return NotFound();
            }

            return View(inwentarz);
        }

        // POST: Inwentarz/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inwentarz == null)
            {
                return Problem("Entity set 'MOBContext.Inwentarz'  is null.");
            }
            var inwentarz = await _context.Inwentarz.FindAsync(id);
            if (inwentarz != null)
            {
                _context.Inwentarz.Remove(inwentarz);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InwentarzExists(int id)
        {
          return (_context.Inwentarz?.Any(e => e.id_inwentarza == id)).GetValueOrDefault();
        }
    }
}
