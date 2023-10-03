using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PatatzaakSoftwareMVC.Data;
using PatatzaakSoftwareMVC.Models;

namespace PatatzaakSoftwareMVC.Controllers
{
    public class VouchersController : Controller
    {
        private readonly MainDb _context;

        public VouchersController(MainDb context)
        {
            _context = context;
        }

        // GET: Vouchers
        public async Task<IActionResult> Index()
        {
            var mainDb = _context.vouchers.Include(v => v.User);
            return View(await mainDb.ToListAsync());
        }

        // GET: Vouchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.vouchers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // GET: Vouchers/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.users, "Id", "UserDisplay");
            return View();
        }

        // POST: Vouchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Price,VoucherDiscount,VoucherCode,ExpiresBy,UserId")] Voucher voucher)
        {            
            _context.Add(voucher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Vouchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.vouchers.FindAsync(id);
            if (voucher == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.users, "Id", "Email", voucher.UserId);
            return View(voucher);
        }

        // POST: Vouchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Price,VoucherDiscount,VoucherCode,ExpiresBy,UserId")] Voucher voucher)
        {
            if (id != voucher.Id)
            {
                return NotFound();
            }
            _context.Update(voucher);
            await _context.SaveChangesAsync();
            
           
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Vouchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.vouchers == null)
            {
                return NotFound();
            }

            var voucher = await _context.vouchers
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (voucher == null)
            {
                return NotFound();
            }

            return View(voucher);
        }

        // POST: Vouchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.vouchers == null)
            {
                return Problem("Entity set 'MainDb.vouchers'  is null.");
            }
            var voucher = await _context.vouchers.FindAsync(id);
            if (voucher != null)
            {
                _context.vouchers.Remove(voucher);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VoucherExists(int id)
        {
          return (_context.vouchers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
