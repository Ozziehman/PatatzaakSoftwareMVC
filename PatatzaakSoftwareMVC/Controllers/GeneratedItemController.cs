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
    public class GeneratedItemController : Controller
    {
        private readonly MainDb _context;

        public GeneratedItemController(MainDb context)
        {
            _context = context;
        }

        // GET: GeneratedItem
        public async Task<IActionResult> Index()
        {
              return _context.items != null ? 
                          View(await _context.items.ToListAsync()) :
                          Problem("Entity set 'MainDb.items'  is null.");
        }

        // GET: GeneratedItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: GeneratedItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GeneratedItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImagePath,Price,Discount")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: GeneratedItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        // POST: GeneratedItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImagePath,Price,Discount")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            return View(item);
        }

        // GET: GeneratedItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.items == null)
            {
                return NotFound();
            }

            var item = await _context.items
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: GeneratedItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.items == null)
            {
                return Problem("Entity set 'MainDb.items'  is null.");
            }
            var item = await _context.items.FindAsync(id);
            if (item != null)
            {
                _context.items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
