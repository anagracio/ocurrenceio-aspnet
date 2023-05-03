using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ocurrenceio_aspnet.Data.Migrations
{
    /// <inheritdoc />
    public partial class modelsCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportFK = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportImage_Report_ReportFK",
                        column: x => x.ReportFK,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportReportState",
                columns: table => new
                {
                    ListReportId = table.Column<int>(type: "int", nullable: false),
                    ListReportStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportReportState", x => new { x.ListReportId, x.ListReportStateId });
                    table.ForeignKey(
                        name: "FK_ReportReportState_ReportState_ListReportStateId",
                        column: x => x.ListReportStateId,
                        principalTable: "ReportState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReportReportState_Report_ListReportId",
                        column: x => x.ListReportId,
                        principalTable: "Report",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportImage_ReportFK",
                table: "ReportImage",
                column: "ReportFK");

            migrationBuilder.CreateIndex(
                name: "IX_ReportReportState_ListReportStateId",
                table: "ReportReportState",
                column: "ListReportStateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReportImage");

            migrationBuilder.DropTable(
                name: "ReportReportState");

            migrationBuilder.DropTable(
                name: "ReportState");

            migrationBuilder.DropTable(
                name: "Report");
        }
    }
}
