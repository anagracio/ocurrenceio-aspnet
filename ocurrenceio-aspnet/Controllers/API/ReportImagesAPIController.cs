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
    public class ReportImagesAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportImagesAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ReportImagesAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportImage>>> GetReportImage()
        {
            return await _context.ReportImage.ToListAsync();
        }

        // GET: api/ReportImagesAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReportImage>> GetReportImage(int id)
        {
            var reportImage = await _context.ReportImage.FindAsync(id);

            if (reportImage == null)
            {
                return NotFound();
            }

            return reportImage;
        }

        // PUT: api/ReportImagesAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReportImage(int id, ReportImage reportImage)
        {
            if (id != reportImage.Id)
            {
                return BadRequest();
            }

            _context.Entry(reportImage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportImageExists(id))
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

        // POST: api/ReportImagesAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReportImage>> PostReportImage(ReportImage reportImage)
        {
            _context.ReportImage.Add(reportImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReportImage", new { id = reportImage.Id }, reportImage);
        }

        // DELETE: api/ReportImagesAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReportImage(int id)
        {
            var reportImage = await _context.ReportImage.FindAsync(id);
            if (reportImage == null)
            {
                return NotFound();
            }

            _context.ReportImage.Remove(reportImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportImageExists(int id)
        {
            return _context.ReportImage.Any(e => e.Id == id);
        }
    }
}
