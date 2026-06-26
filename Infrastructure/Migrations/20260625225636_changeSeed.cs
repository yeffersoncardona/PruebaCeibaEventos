using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "Descripción de la conferencia de tecnología", new DateTime(2026, 7, 25, 16, 0, 0, 0, DateTimeKind.Utc), 200m, new DateTime(2026, 7, 25, 14, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "Descripción del taller de fotografía", new DateTime(2026, 8, 10, 11, 0, 0, 0, DateTimeKind.Utc), 100m, new DateTime(2026, 8, 10, 9, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "Descripción del concierto de rock", new DateTime(2026, 8, 25, 22, 0, 0, 0, DateTimeKind.Utc), 500m, new DateTime(2026, 8, 25, 20, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "", new DateTime(2026, 7, 26, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(7087), 0m, new DateTime(2026, 7, 25, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(6739) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "", new DateTime(2026, 8, 10, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(8072), 0m, new DateTime(2026, 8, 9, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "Description", "EndDate", "Price", "StartDate" },
                values: new object[] { "", new DateTime(2026, 8, 25, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(8076), 0m, new DateTime(2026, 8, 24, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(8076) });
        }
    }
}
