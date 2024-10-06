using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCMS.API.Migrations.EvidenceDb
{
    /// <inheritdoc />
    public partial class InitialCreate_FirstDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestAudits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestAudits", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestAudits");
        }
    }
}
