using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCMS.API.Migrations
{
    /// <inheritdoc />
    public partial class Efgr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Locations_LocationId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationId",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Locations_LocationId",
                table: "Bookings",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
