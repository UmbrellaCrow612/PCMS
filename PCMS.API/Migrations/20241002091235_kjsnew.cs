using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCMS.API.Migrations
{
    /// <inheritdoc />
    public partial class kjsnew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_AspNetUsers_LastEditedById",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_LastEditedById",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LastEditedById",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Cases_LastEditedById",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Evidences");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Reports",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastEditedById",
                table: "Reports",
                newName: "LastModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reports",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Evidences",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "Cases",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "LastEditedById",
                table: "Cases",
                newName: "LastModifiedAtUtc");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "CaseNotes",
                newName: "LastModifiedById");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "CaseNotes",
                newName: "CreatedAtUtc");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Reports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Reports",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "Evidences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Evidences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Evidences",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAtUtc",
                table: "Evidences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedById",
                table: "Evidences",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAtUtc",
                table: "Cases",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAtUtc",
                table: "CaseNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "CaseNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CaseNotes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAtUtc",
                table: "CaseNotes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_DeletedById",
                table: "Reports",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_IsDeleted",
                table: "Reports",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LastModifiedById",
                table: "Reports",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_DeletedById",
                table: "Evidences",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_IsDeleted",
                table: "Evidences",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_LastModifiedById",
                table: "Evidences",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_LastModifiedById",
                table: "Cases",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_DeletedById",
                table: "CaseNotes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_IsDeleted",
                table: "CaseNotes",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_LastModifiedById",
                table: "CaseNotes",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseNotes_AspNetUsers_DeletedById",
                table: "CaseNotes",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseNotes_AspNetUsers_LastModifiedById",
                table: "CaseNotes",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_AspNetUsers_LastModifiedById",
                table: "Cases",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_AspNetUsers_DeletedById",
                table: "Evidences",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evidences_AspNetUsers_LastModifiedById",
                table: "Evidences",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_DeletedById",
                table: "Reports",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_LastModifiedById",
                table: "Reports",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseNotes_AspNetUsers_DeletedById",
                table: "CaseNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_CaseNotes_AspNetUsers_LastModifiedById",
                table: "CaseNotes");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_AspNetUsers_LastModifiedById",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_AspNetUsers_DeletedById",
                table: "Evidences");

            migrationBuilder.DropForeignKey(
                name: "FK_Evidences_AspNetUsers_LastModifiedById",
                table: "Evidences");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_DeletedById",
                table: "Reports");

            migrationBuilder.DropForeignKey(
                name: "FK_Reports_AspNetUsers_LastModifiedById",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_DeletedById",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_IsDeleted",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Reports_LastModifiedById",
                table: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Evidences_DeletedById",
                table: "Evidences");

            migrationBuilder.DropIndex(
                name: "IX_Evidences_IsDeleted",
                table: "Evidences");

            migrationBuilder.DropIndex(
                name: "IX_Evidences_LastModifiedById",
                table: "Evidences");

            migrationBuilder.DropIndex(
                name: "IX_Cases_LastModifiedById",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_CaseNotes_DeletedById",
                table: "CaseNotes");

            migrationBuilder.DropIndex(
                name: "IX_CaseNotes_IsDeleted",
                table: "CaseNotes");

            migrationBuilder.DropIndex(
                name: "IX_CaseNotes_LastModifiedById",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "LastModifiedAtUtc",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "LastModifiedById",
                table: "Evidences");

            migrationBuilder.DropColumn(
                name: "CreatedAtUtc",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "DeletedAtUtc",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CaseNotes");

            migrationBuilder.DropColumn(
                name: "LastModifiedAtUtc",
                table: "CaseNotes");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Reports",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAtUtc",
                table: "Reports",
                newName: "LastEditedById");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Reports",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "Evidences",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Cases",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "LastModifiedAtUtc",
                table: "Cases",
                newName: "LastEditedById");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "CaseNotes",
                newName: "LastModifiedDate");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "CaseNotes",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Evidences",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LastEditedById",
                table: "Reports",
                column: "LastEditedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_LastEditedById",
                table: "Cases",
                column: "LastEditedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_AspNetUsers_LastEditedById",
                table: "Cases",
                column: "LastEditedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reports_AspNetUsers_LastEditedById",
                table: "Reports",
                column: "LastEditedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
