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
    public class ReportsController : Controller
    {
        /// <summary>
        /// reference the application database
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// all data about web hosting environment
        /// </summary>
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Reports controller constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="webHostEnvironment"></param>
        public ReportsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            return View(await _context.Report.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Report == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.ListReportImage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Latitude,Longitude")] Report report, List<IFormFile> images)
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

            if (ModelState.IsValid)
            {
                // Check if the image is valid, if not, return the view with the error message
                if (imgValidationFlag)
                {
                    ModelState.AddModelError(string.Empty, "The image must be a JPEG, PNG or JPG.");
                    return View(report);
                }

                // Add the report to the context, but don't save changes yet
                _context.Add(report);

                try
                {
                    await _context.SaveChangesAsync();

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
                        }
                        catch (Exception ex)
                        {
                            // Handle any exceptions that occur during the image saving process
                            ModelState.AddModelError(string.Empty, $"Error saving image: {ex.Message}");
                        }

                    }
                    // Save all changes to the database
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur during saving changes to the database
                    ModelState.AddModelError(string.Empty, $"Error saving changes: {ex.Message}");
                }
            }
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Report == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .Include(r => r.ListReportImage)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Latitude,Longitude")] Report report, List<IFormFile> images, int[] deleteImageIds)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.Id))
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
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Report == null)
            {
                return NotFound();
            }

            var report = await _context.Report
                .FirstOrDefaultAsync(m => m.Id == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Report == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Report'  is null.");
            }
            var report = await _context.Report.FindAsync(id);
            if (report != null)
            {
                _context.Report.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Report.Any(e => e.Id == id);
        }
    }
}
