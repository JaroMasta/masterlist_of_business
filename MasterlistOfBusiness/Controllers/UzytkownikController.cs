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
using Microsoft.AspNetCore.Authorization;


namespace MasterlistOfBusiness.Controllers
{
    [Authorize (Roles = "Admin")] // Ensure only Admins can access this controller, but login and logout is allowed for all users
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
                        View(await _context.Uzytkownik.ToListAsync()) :
                        Problem("Entity set 'MOBContext.Uzytkownik'  is null.");
        }

        // GET: Uzytkownik/Details/5
        public async Task<IActionResult> Details(string id)
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
        public async Task<IActionResult> Create([Bind("login,Haslo,typ")] Uzytkownik uzytkownik)
        {
            if (ModelState.IsValid)
            {
                // Hash the password before saving
                uzytkownik.HasloHash = BCrypt.Net.BCrypt.HashPassword(uzytkownik.Haslo);
                // Clear the plain text password
                uzytkownik.Haslo = string.Empty;

                _context.Add(uzytkownik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            { 
                Console.WriteLine("ModelState is invalid. Errors:");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"- {error.ErrorMessage}");
                }
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
        public async Task<IActionResult> Edit(string id, [Bind("login,HasloHash,typ")] Uzytkownik uzytkownik)
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
        [AllowAnonymous]
        public IActionResult Login() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string login, string haslo)
        {
            Console.WriteLine($"Login POST called with login={login}");
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
                Console.WriteLine($"Zalogowano użytkownika: {user.login} ({user.typ})");
                if (user.typ == "Admin")
                {
                    return RedirectToAction("Index", "Uzytkownik"); // Redirect to Uzytkownik index for Admins
                }
                else
                {
                    return RedirectToAction("Index", "Home"); // Redirect to Home index for regular users
                }    return RedirectToAction("Index", "Home");
            }
            if (user == null)
            {
                Console.WriteLine("Nie znaleziono użytkownika.");
            }
            else if (!BCrypt.Net.BCrypt.Verify(haslo, user.HasloHash))
            {
                Console.WriteLine("Hasło nieprawidłowe.");
            }
            else
            {
                Console.WriteLine("Wystąpił nieoczekiwany błąd.");
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
