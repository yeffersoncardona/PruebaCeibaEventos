using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addcolumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReservationCode",
                table: "Reservations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "Capacity", "Description", "EndDate", "Price", "StartDate", "Status", "Title", "Type", "VenueId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), 200, "", new DateTime(2026, 7, 26, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(7087), 0m, new DateTime(2026, 7, 25, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(6739), 0, "Conferencia de Tecnología", "conferencia", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 50, "", new DateTime(2026, 8, 10, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(8072), 0m, new DateTime(2026, 8, 9, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(8071), 0, "Taller de Fotografía", "taller", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 500, "", new DateTime(2026, 8, 25, 0, 41, 32, 836, DateTimeKind.Utc).AddTicks(8076), 0m, new DateTime(2026, 8, 24, 22, 41, 32, 836, DateTimeKind.Utc).AddTicks(8076), 0, "Concierto de Rock", "concierto", new Guid("33333333-3333-3333-3333-333333333333") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Events");

            migrationBuilder.AlterColumn<string>(
                name: "ReservationCode",
                table: "Reservations",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
