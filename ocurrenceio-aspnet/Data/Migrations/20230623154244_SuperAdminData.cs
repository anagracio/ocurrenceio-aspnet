using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ocurrenceio_aspnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class SuperAdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "02174cf0-9412-4cfe-afbf-59f706d72cf6", 0, "0f2e0340-2dcf-4b3b-9d9d-e50f2c4466b3", "joca@occurrence.io", true, false, null, "joca@occurrence.io", "joca@occurrence.io", "AQAAAAIAAYagAAAAEDfzzY4V7x13WD/6eDcqKylFUMJisutyIA54cOZFYtjSmBY47hXqdaA3kbwuyQzIbA==", null, false, "", false, "joca@occurrence.io" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a", "02174cf0-9412-4cfe-afbf-59f706d72cf6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a", "02174cf0-9412-4cfe-afbf-59f706d72cf6" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "02174cf0-9412-4cfe-afbf-59f706d72cf6");
        }
    }
}
