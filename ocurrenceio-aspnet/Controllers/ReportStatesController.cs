using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ocurrenceio_aspnet.Data;
using ocurrenceio_aspnet.Models;

namespace ocurrenceio_aspnet.Controllers
{
    public class ReportStatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportStatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReportStates
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.ReportState.ToListAsync());
        }

        // GET: ReportStates/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReportStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,State")] ReportState reportState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reportState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reportState);
        }

        // GET: ReportStates/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReportState == null)
            {
                return NotFound();
            }

            var reportState = await _context.ReportState.FindAsync(id);
            if (reportState == null)
            {
                return NotFound();
            }
            return View(reportState);
        }

        // POST: ReportStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,State")] ReportState reportState)
        {
            if (id != reportState.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reportState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportStateExists(reportState.Id))
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
            return View(reportState);
        }

        // GET: ReportStates/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReportState == null)
            {
                return NotFound();
            }

            var reportState = await _context.ReportState
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reportState == null)
            {
                return NotFound();
            }

            return View(reportState);
        }

        // POST: ReportStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReportState == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ReportState'  is null.");
            }
            var reportState = await _context.ReportState.FindAsync(id);
            if (reportState != null)
            {
                _context.ReportState.Remove(reportState);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportStateExists(int id)
        {
          return _context.ReportState.Any(e => e.Id == id);
        }
    }
}
