using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MasterlistOfBusiness.Data;
using MasterlistOfBusiness.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MasterlistOfBusiness.Controllers
{
    public class UzytkownikController : Controller
    {
        private readonly MOBContext _context;

        public UzytkownikController(MOBContext context)
        {
            _context = context;
        }

        // GET: Uzytkownik
        public async Task<IActionResult> Index()
        {
              return _context.Uzytkownik != null ? 
                          View(await _context.Uzytkownik.Include(u => u.Sprzedawca).AsNoTracking().ToListAsync()) :
                          Problem("Entity set 'MOBContext.Uzytkownik'  is null.");
        }

        // GET: Uzytkownik/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik.Include(u => u.Sprzedawca)
                .FirstOrDefaultAsync(m => m.login == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }

        private void PopulateSprzedawcaDropDownList(object selectedSprzedawca = null)
        {
            var Sprzedawca = from e in _context.Sprzedawca
                                orderby e.login
                                select e;
            var res = Sprzedawca.AsNoTracking();
            ViewBag.SprzedawcaID = new SelectList(res, "Id", "Nazwa", selectedSprzedawca);
        }

        // GET: Uzytkownik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uzytkownik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("login,haslo,typ")] Uzytkownik uzytkownik, IFormCollection form)
        {
            string sprzedawcaValue = form["Sprzedawca"].ToString();
            if (ModelState.IsValid)
            {
                Sprzedawca sprzedawca = null;
                if (sprzedawcaValue != "-1")
                {
                    var k = _context.Sprzedawca.Where(k => k.id_sprzedawcy == int.Parse(sprzedawcaValue));
                    if (k.Count() > 0)
                        sprzedawca = k.First();
                }
                _context.Add(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            return View(uzytkownik);
        }

        // POST: Uzytkownik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("login,haslo,typ")] Uzytkownik uzytkownik)
        {
            if (id != uzytkownik.login)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uzytkownik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UzytkownikExists(uzytkownik.login))
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
            return View(uzytkownik);
        }

        // GET: Uzytkownik/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Uzytkownik == null)
            {
                return NotFound();
            }

            var uzytkownik = await _context.Uzytkownik
                .FirstOrDefaultAsync(m => m.login == id);
            if (uzytkownik == null)
            {
                return NotFound();
            }

            return View(uzytkownik);
        }

        // POST: Uzytkownik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Uzytkownik == null)
            {
                return Problem("Entity set 'MOBContext.Uzytkownik'  is null.");
            }
            var uzytkownik = await _context.Uzytkownik.FindAsync(id);
            if (uzytkownik != null)
            {
                _context.Uzytkownik.Remove(uzytkownik);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UzytkownikExists(string id)
        {
            return (_context.Uzytkownik?.Any(e => e.login == id)).GetValueOrDefault();
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string login, string haslo)
        {
            var user = await _context.Uzytkownik.FirstOrDefaultAsync(u => u.login == login);
            if (user != null && BCrypt.Net.BCrypt.Verify(haslo, user.HasloHash))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.login),
                    new Claim(ClaimTypes.Role, user.typ ?? "User")
                };

                var identity = new ClaimsIdentity(claims, "MyCookieAuth");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("MyCookieAuth", principal);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Nieprawidłowy login lub hasło");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login");
        }
    }
}
