using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ocurrenceio_aspnet.Models;

namespace ocurrenceio_aspnet.Data {
    public class ApplicationDbContext : IdentityDbContext {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {
        }

        /// <summary>
        /// it executes code before the creation of model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            // imports the previous execution of this method
            base.OnModelCreating(modelBuilder);
            //*****************************************
            // add, at this point, your new code

            // seed of ReportState data
            modelBuilder.Entity<ReportState>().HasData(
              new ReportState { Id = 1, State = "Pending" },
              new ReportState { Id = 2, State = "In Progress" },
              new ReportState { Id = 3, State = "Done" }
            );

            // seed the Roles data
            modelBuilder.Entity<IdentityRole>().HasData(
              new IdentityRole { Id = "u", Name = "User", NormalizedName = "USER" },
              new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
            );
        }

        // Definition of project's tables
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportImage> ReportImage { get; set; } 
        public DbSet<ReportState> ReportState { get; set; }
    }
}