using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
              return View(await _context.ReportState.ToListAsync());
        }

        private bool ReportStateExists(int id)
        {
          return _context.ReportState.Any(e => e.Id == id);
        }
    }
}
