using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MorphyWallet.Data;
using MorphyWallet.Models;

namespace MorphyWallet.Controllers
{
    public class WalletsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalletsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wallets
        public async Task<IActionResult> Index()
        {
            var wallets = _context.Wallets.Include(w => w.User);
            return View(await wallets.ToListAsync());
        }

        // GET: Wallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var wallet = await _context.Wallets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wallet == null)
                return NotFound();

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Wallets/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,PlanName,Balance,Limit,Description,IsActive")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                wallet.CreatedAt = DateTime.UtcNow;
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Catalogue));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
                return NotFound();

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,PlanName,Balance,Limit,Description,IsActive")] Wallet wallet)
        {
            if (id != wallet.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletExists(wallet.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Catalogue));
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var wallet = await _context.Wallets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (wallet == null)
                return NotFound();

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Catalogue));
        }

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }

        // GET: Wallets/Catalogue
        public async Task<IActionResult> Catalogue()
        {
            var catalogueWallets = await _context.Wallets
                .Include(w => w.User)
                .Where(w => !string.IsNullOrEmpty(w.PlanName))
                .OrderByDescending(w => w.CreatedAt)
                .ToListAsync();

            return View(catalogueWallets);
        }
    }
}
