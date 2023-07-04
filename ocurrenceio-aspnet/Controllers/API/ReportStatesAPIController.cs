using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ocurrenceio_aspnet.Data;
using ocurrenceio_aspnet.Models;

namespace ocurrenceio_aspnet.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportStatesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportStatesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportStatesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportState>>> GetReportState()
        {
            return await _context.ReportState.ToListAsync();
        }

        private bool ReportStateExists(int id)
        {
            return _context.ReportState.Any(e => e.Id == id);
        }
    }
}
