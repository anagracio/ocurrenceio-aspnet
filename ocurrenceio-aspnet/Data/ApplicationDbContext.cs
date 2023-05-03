using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ocurrenceio_aspnet.Models;

namespace ocurrenceio_aspnet.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        // Definition of project's table
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportImage> ReportImage { get; set; } 
        public DbSet<ReportState> ReportState { get; set; }
    }
}