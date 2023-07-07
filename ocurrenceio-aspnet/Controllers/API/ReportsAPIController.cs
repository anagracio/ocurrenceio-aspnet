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
    public class ReportsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportsAPIController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET: api/ReportsAPI
        /// Returns all reports
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReport()
        {
            // get all reports
            var reports = await _context.Report
                .Include(r => r.ListReportImage)
                .ToListAsync();

            // get the state of each report
            foreach (var report in reports)
            {
                report.ListReportState = await _context.ReportState
                    .Where(s => s.ListReport.Any(r => r.Id == report.Id))
                    .ToListAsync();
            }

            return reports;
        }

        /// <summary>
        /// GET: api/ReportsAPI/5
        /// Returns a specific report given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(int id)
        {
            // get the report
            var report = await _context.Report
                .Include(r => r.ListReportImage)
                .Include(r => r.ListReportState)
                .FirstOrDefaultAsync(r => r.Id == id);

            // check if the report exists
            if (report == null)
            {
                return NotFound();
            }

            return report;
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// PUT: api/ReportsAPI/5
        /// Updates a specific report given an id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="report"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport([FromForm] int id, [FromForm] Report report, [FromForm] List<IFormFile> images)
        {
            // variable to check if the image is valid
            var imgValidationFlag = false;
            // validates each image, if one of them is not valid, the flag is set to true
            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    if (!(image.ContentType == "image/jpeg" || image.ContentType == "image/png" || image.ContentType == "image/jpg"))
                    {
                        imgValidationFlag = true;
                        break;
                    }
                }
            }

            // Check if the image is valid, if not, return the view with the error message
            if (imgValidationFlag)
            {
                return BadRequest("A imagem deve ter o formato JPEG, PNG ou JPG.");
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest("Erro ao editar o report.");
                }
            }

            // if the image is valid, save it to the database
            // Process uploaded images and associate them with the new report
            foreach (var image in images)
            {
                // Generate a unique filename for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // Define the path where the image will be saved
                var imagePath = Path.Combine("Photos", fileName);

                // Save the image file to the server
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

                try
                {
                    // Save the image file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Create a new ReportImage object and associate it with the new report
                    var reportImage = new ReportImage
                    {
                        ReportFK = report.Id,
                        Name = imagePath
                    };
                    _context.ReportImage.Add(reportImage);
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {
                    // Handle any exceptions that occur during the image saving process
                    return BadRequest("Erro ao guardar a imagem.");
                }

            }

            return NoContent();
        }


        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// POST: api/ReportsAPI
        /// Creates a new report
        /// </summary>
        /// <param name="report"></param>
        /// <param name="images"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport([FromForm] Report report, [FromForm] List<IFormFile> images)
        {   
            // variable to check if the image is valid
            var imgValidationFlag = false;
            // validates each image, if one of them is not valid, the flag is set to true
            foreach (var image in images)
            {
                if (image != null && image.Length > 0)
                {
                    if (!(image.ContentType == "image/jpeg" || image.ContentType == "image/png" || image.ContentType == "image/jpg"))
                    {
                        imgValidationFlag = true;
                        break;
                    }
                }
            }

            // Check if the image is valid, if not, return the view with the error message
            if (imgValidationFlag)
            {
                return BadRequest("A imagem deve ter o formato JPEG, PNG ou JPG.");
            }

            try
            {
                _context.Report.Add(report);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return BadRequest("Erro ao guardar os dados.");
            }

            // if the image is valid, save it to the database
            // Process uploaded images and associate them with the new report
            foreach (var image in images)
            {
                // Generate a unique filename for the image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

                // Define the path where the image will be saved
                var imagePath = Path.Combine("Photos", fileName);

                // Save the image file to the server
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagePath);

                try
                {
                    // Save the image file to the server
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Create a new ReportImage object and associate it with the new report
                    var reportImage = new ReportImage
                    {
                        ReportFK = report.Id,
                        Name = imagePath
                    };
                    _context.ReportImage.Add(reportImage);
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {
                    // Handle any exceptions that occur during the image saving process
                    return BadRequest("Erro ao guardar a imagem.");
                }

            }

            // Set the initial state of the report to "pending"
            var initialState = await _context.ReportState.FirstOrDefaultAsync(s => s.Id == 1);
            if (initialState == null)
            {
                return BadRequest("Este estado não existe. Por favor, contacte o seu departamento IT.");
            }
            report.ListReportState.Add(initialState);
            await _context.SaveChangesAsync();


            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// POST: api/ReportsAPI/ChangeReportState/5
        /// Changes the state of a specific report given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("ChangeReportState/{id}")]
        public async Task<ActionResult<Report>> ChangeReportState([FromForm] int id)
        {
            var report = await _context.Report
                .Include(r => r.ListReportState)
                .FirstOrDefaultAsync(r => r.Id == id);

            // Check if the report exists
            if (report == null)
            {
                return NotFound(); // or return any appropriate response indicating that the report was not found
            }

            // if report does exists
            if (report != null)
            {
                // add a new state to the report
                // if the state is "pending", add the "in progress" state
                // if the state is "in progress", add the "done" state
                // if the state is "done", doesn't do anything
                if (report.ListReportState.LastOrDefault().Id == 1)
                {
                    var state = await _context.ReportState.FirstOrDefaultAsync(s => s.Id == 2);
                    if (state == null)
                    {
                        return BadRequest("Este estado não existe. Por favor, contacte o seu departamento IT.");
                    }
                    report.ListReportState.Add(state);
                }
                else if (report.ListReportState.LastOrDefault().Id == 2)
                {
                    var state = await _context.ReportState.FirstOrDefaultAsync(s => s.Id == 3);
                    if (state == null)
                    {
                        return BadRequest("Este estado não existe. Por favor, contacte o seu departamento IT.");
                    }
                    report.ListReportState.Add(state);
                }

                try
                {
                    // Save all changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (System.Exception)
                {
                    // Handle any exceptions that occur during saving changes to the database
                    return BadRequest("Erro ao guardar a alteração do estado.");
                }
            }

            return Ok();
        }

        /// <summary>
        /// DELETE: api/ReportsAPI/5
        /// Deletes a specific report given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(int id)
        {
            var report = await _context.Report.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Report.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Checks if a specific report exists given an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
