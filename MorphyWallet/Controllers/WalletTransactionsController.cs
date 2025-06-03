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
    public class WalletTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WalletTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WalletTransactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WalletTransactions.Include(w => w.Wallet);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WalletTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletTransaction = await _context.WalletTransactions
                .Include(w => w.Wallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (walletTransaction == null)
            {
                return NotFound();
            }

            return View(walletTransaction);
        }

        // GET: WalletTransactions/Create
        public IActionResult Create()
        {
            ViewData["WalletId"] = new SelectList(_context.Wallets, "Id", "UserId");
            return View();
        }

        // POST: WalletTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WalletId,TransactionType,Amount,Timestamp")] WalletTransaction walletTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(walletTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WalletId"] = new SelectList(_context.Wallets, "Id", "UserId", walletTransaction.WalletId);
            return View(walletTransaction);
        }

        // GET: WalletTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletTransaction = await _context.WalletTransactions.FindAsync(id);
            if (walletTransaction == null)
            {
                return NotFound();
            }
            ViewData["WalletId"] = new SelectList(_context.Wallets, "Id", "UserId", walletTransaction.WalletId);
            return View(walletTransaction);
        }

        // POST: WalletTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WalletId,TransactionType,Amount,Timestamp")] WalletTransaction walletTransaction)
        {
            if (id != walletTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(walletTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WalletTransactionExists(walletTransaction.Id))
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
            ViewData["WalletId"] = new SelectList(_context.Wallets, "Id", "UserId", walletTransaction.WalletId);
            return View(walletTransaction);
        }

        // GET: WalletTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var walletTransaction = await _context.WalletTransactions
                .Include(w => w.Wallet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (walletTransaction == null)
            {
                return NotFound();
            }

            return View(walletTransaction);
        }

        // POST: WalletTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var walletTransaction = await _context.WalletTransactions.FindAsync(id);
            if (walletTransaction != null)
            {
                _context.WalletTransactions.Remove(walletTransaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WalletTransactionExists(int id)
        {
            return _context.WalletTransactions.Any(e => e.Id == id);
        }
    }
}
