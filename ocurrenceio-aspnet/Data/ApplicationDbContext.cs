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
            const string ADMIN_ID = "02174cf0-9412-4cfe-afbf-59f706d72cf6";

            // seed the Roles data
            modelBuilder.Entity<IdentityRole>().HasData(
              new IdentityRole { Id = "u", Name = "User", NormalizedName = "USER" },
              new IdentityRole { Id = "a", Name = "Admin", NormalizedName = "ADMIN" }
            );

            // Hash password
            var hasher = new PasswordHasher<IdentityUser>();

            //create user and seed user
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser
            {
                Id = ADMIN_ID,
                UserName = "joca@occurrence.io",
                NormalizedUserName = "joca@occurrence.io",
                Email = "joca@occurrence.io",
                NormalizedEmail = "joca@occurrence.io",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "d1sIsG0dM0d3!"),
                SecurityStamp = string.Empty
            });

            // set user role to admin
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "a",
                UserId = ADMIN_ID
            });


        }

        // Definition of project's tables
        public DbSet<Report> Report { get; set; }
        public DbSet<ReportImage> ReportImage { get; set; } 
        public DbSet<ReportState> ReportState { get; set; }
    }
}