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
    public class PoliticiansController : Controller
    {
        private readonly KravateDbContext _context;

        public PoliticiansController(KravateDbContext context)
        {
            _context = context;    
        }

        // GET: Politicians
        public async Task<IActionResult> Index()
        {
            return View(await _context.Politician.ToListAsync());
        }

        // GET: Politicians/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politician = await _context.Politician.SingleOrDefaultAsync(m => m.ID == id);
            if (politician == null)
            {
                return NotFound();
            }

            return View(politician);
        }

        // GET: Politicians/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Politicians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,PartyID,Popularity,Surname,VoteCount")] Politician politician)
        {
            if (ModelState.IsValid)
            {
                politician.ID = Guid.NewGuid();
                _context.Add(politician);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(politician);
        }

        // GET: Politicians/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politician = await _context.Politician.SingleOrDefaultAsync(m => m.ID == id);
            if (politician == null)
            {
                return NotFound();
            }
            return View(politician);
        }

        // POST: Politicians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Name,PartyID,Popularity,Surname,VoteCount")] Politician politician)
        {
            if (id != politician.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(politician);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliticianExists(politician.ID))
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
            return View(politician);
        }

        // GET: Politicians/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var politician = await _context.Politician.SingleOrDefaultAsync(m => m.ID == id);
            if (politician == null)
            {
                return NotFound();
            }

            return View(politician);
        }

        // POST: Politicians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var politician = await _context.Politician.SingleOrDefaultAsync(m => m.ID == id);
            _context.Politician.Remove(politician);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PoliticianExists(Guid id)
        {
            return _context.Politician.Any(e => e.ID == id);
        }
    }
}
