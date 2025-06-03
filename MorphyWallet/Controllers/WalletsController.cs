using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            var applicationDbContext = _context.Wallets.Include(w => w.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Wallets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // GET: Wallets/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Wallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Balance")] Wallet wallet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // GET: Wallets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // POST: Wallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Balance")] Wallet wallet)
        {
            if (id != wallet.Id)
            {
                return NotFound();
            }

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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", wallet.UserId);
            return View(wallet);
        }

        // GET: Wallets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wallet = await _context.Wallets
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wallet == null)
            {
                return NotFound();
            }

            return View(wallet);
        }

        // POST: Wallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wallet = await _context.Wallets.FindAsync(id);
            if (wallet != null)
            {
                _context.Wallets.Remove(wallet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletExists(int id)
        {
            return _context.Wallets.Any(e => e.Id == id);
        }
        public IActionResult Catalogue()
        {
            var demoWallets = new List<dynamic>
    {
        new {
            PlanId = 1,
            PlanName = "Starter",
            Balance = 100,
            Limit = 1000,
            Description = "Entry-level wallet plan",
            IsActive = true,
            Created = DateTime.UtcNow.AddDays(-10)
        },
        new {
            PlanId = 2,
            PlanName = "Basic",
            Balance = 250,
            Limit = 2000,
            Description = "Basic monthly wallet",
            IsActive = true,
            Created = DateTime.UtcNow.AddDays(-20)
        },
        new {
            PlanId = 3,
            PlanName = "Standard",
            Balance = 500,
            Limit = 5000,
            Description = "Standard user plan",
            IsActive = true,
            Created = DateTime.UtcNow.AddDays(-30)
        },
        new {
            PlanId = 4,
            PlanName = "Premium",
            Balance = 750,
            Limit = 10000,
            Description = "Premium user wallet",
            IsActive = false,
            Created = DateTime.UtcNow.AddDays(-40)
        },
        new {
            PlanId = 5,
            PlanName = "Enterprise",
            Balance = 1000,
            Limit = 25000,
            Description = "For business users",
            IsActive = true,
            Created = DateTime.UtcNow.AddDays(-50)
        }
    };

            return View(demoWallets);
        }

    }
}
