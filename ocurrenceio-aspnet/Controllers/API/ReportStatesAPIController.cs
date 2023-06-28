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

        // GET: api/ReportStatesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportState>> GetReportState(int id)
        {
            var reportState = await _context.ReportState.FindAsync(id);

            if (reportState == null)
            {
                return NotFound();
            }

            return reportState;
        }

        // PUT: api/ReportStatesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportState(int id, ReportState reportState)
        {
            if (id != reportState.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportState).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportStateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ReportStatesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportState>> PostReportState(ReportState reportState)
        {
            _context.ReportState.Add(reportState);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportState", new { id = reportState.Id }, reportState);
        }

        // DELETE: api/ReportStatesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportState(int id)
        {
            var reportState = await _context.ReportState.FindAsync(id);
            if (reportState == null)
            {
                return NotFound();
            }

            _context.ReportState.Remove(reportState);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportStateExists(int id)
        {
            return _context.ReportState.Any(e => e.Id == id);
        }
    }
}
