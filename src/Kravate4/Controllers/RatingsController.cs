using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kravate4.Data;
using Kravate4.Models;

namespace Kravate4.Controllers
{
    public class RatingsController : Controller
    {
        private readonly KravateDbContext _context;

        public RatingsController(KravateDbContext context)
        {
            _context = context;    
        }

        // GET: Ratings
        public async Task<IActionResult> Index()
        {
            var kravateDbContext = _context.Rating.Include(r => r.Politician);
            return View(await kravateDbContext.ToListAsync());
        }

        // GET: Ratings/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Ratings/Create
        public IActionResult Create()
        {
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID");
            return View();
        }

        // POST: Ratings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateCreated,DateUpdated,PoliticianID,UserID,Value")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                rating.ID = Guid.NewGuid();
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "Name", "Name", rating.PoliticianID);
            return View(rating);
        }

        // GET: Ratings/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", rating.PoliticianID);
            return View(rating);
        }

        // POST: Ratings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,DateCreated,DateUpdated,PoliticianID,UserID,Value")] Rating rating)
        {
            if (id != rating.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PoliticianID"] = new SelectList(_context.Politician, "ID", "ID", rating.PoliticianID);
            return View(rating);
        }

        // GET: Ratings/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Ratings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rating = await _context.Rating.SingleOrDefaultAsync(m => m.ID == id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool RatingExists(Guid id)
        {
            return _context.Rating.Any(e => e.ID == id);
        }
    }
}
