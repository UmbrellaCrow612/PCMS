using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCMS.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ShortCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Latitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    Longitude = table.Column<decimal>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Make = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    VIN = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    LicensePlate = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Rank = table.Column<string>(type: "TEXT", nullable: false),
                    BadgeNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DepartmentId = table.Column<string>(type: "TEXT", nullable: true),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CrimeScenes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ReportedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DiscoveredDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LocationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeScenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrimeScenes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    PropertyType = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Bedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    Bathrooms = table.Column<decimal>(type: "TEXT", nullable: false),
                    Area = table.Column<decimal>(type: "TEXT", nullable: false),
                    YearBuilt = table.Column<int>(type: "INTEGER", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    LocationId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Properties_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CaseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Complexity = table.Column<int>(type: "INTEGER", nullable: false),
                    DateOpened = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Priority = table.Column<int>(type: "INTEGER", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true),
                    DeletedById = table.Column<string>(type: "TEXT", nullable: true),
                    DepartmentId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cases_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    ContactInfo = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tags_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserCases",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserCases", x => new { x.UserId, x.CaseId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserCases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserCases_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseActions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseActions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseActions_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaseActions_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseEdits",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PreviousTitle = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousDescription = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    PreviousPriority = table.Column<int>(type: "INTEGER", nullable: false),
                    PreviousType = table.Column<string>(type: "TEXT", nullable: false),
                    PreviousComplexity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseEdits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseEdits_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseEdits_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseNotes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedById = table.Column<string>(type: "TEXT", nullable: true),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseNotes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseNotes_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaseNotes_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CaseNotes_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseVehicles",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    VehicleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseVehicles", x => new { x.CaseId, x.VehicleId });
                    table.ForeignKey(
                        name: "FK_CaseVehicles_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseVehicles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrimeSceneCases",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    CrimeSceneId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeSceneCases", x => new { x.CaseId, x.CrimeSceneId });
                    table.ForeignKey(
                        name: "FK_CrimeSceneCases_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeSceneCases_CrimeScenes_CrimeSceneId",
                        column: x => x.CrimeSceneId,
                        principalTable: "CrimeScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evidences",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FileUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    CollectedByDetails = table.Column<string>(type: "TEXT", nullable: false),
                    CollectionDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidences_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evidences_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evidences_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Evidences_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Details = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    DeletedById = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_DeletedById",
                        column: x => x.DeletedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LastModifiedAtUtc = table.Column<DateTime>(type: "TEXT", nullable: true),
                    CreatedById = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseId = table.Column<string>(type: "TEXT", nullable: true),
                    LocationId = table.Column<string>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    LastModifiedById = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_AspNetUsers_LastModifiedById",
                        column: x => x.LastModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Bookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CasePersons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CasePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CasePersons_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CasePersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrimeScenePersons",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    CrimeSceneId = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrimeScenePersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CrimeScenePersons_CrimeScenes_CrimeSceneId",
                        column: x => x.CrimeSceneId,
                        principalTable: "CrimeScenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrimeScenePersons_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseTags",
                columns: table => new
                {
                    CaseId = table.Column<string>(type: "TEXT", nullable: false),
                    TagId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTags", x => new { x.TagId, x.CaseId });
                    table.ForeignKey(
                        name: "FK_CaseTags_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseTags_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charges",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Offense = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    DateCharged = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    BookingId = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charges_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charges_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charges_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    BookingId = table.Column<string>(type: "TEXT", nullable: false),
                    PersonId = table.Column<string>(type: "TEXT", nullable: false),
                    ReleaseType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Releases_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Releases_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Releases_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserCases_CaseId",
                table: "ApplicationUserCases",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_CreatedById",
                table: "Bookings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Id",
                table: "Bookings",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LastModifiedById",
                table: "Bookings",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_LocationId",
                table: "Bookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PersonId",
                table: "Bookings",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseActions_CaseId",
                table: "CaseActions",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseActions_CreatedById",
                table: "CaseActions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseActions_Id",
                table: "CaseActions",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseActions_LastModifiedById",
                table: "CaseActions",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEdits_CaseId",
                table: "CaseEdits",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEdits_CreatedById",
                table: "CaseEdits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEdits_Id",
                table: "CaseEdits",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_CaseId",
                table: "CaseNotes",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_CreatedById",
                table: "CaseNotes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_DeletedById",
                table: "CaseNotes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_Id",
                table: "CaseNotes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_IsDeleted",
                table: "CaseNotes",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CaseNotes_LastModifiedById",
                table: "CaseNotes",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CasePersons_CaseId",
                table: "CasePersons",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CasePersons_Id",
                table: "CasePersons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CasePersons_PersonId",
                table: "CasePersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseNumber",
                table: "Cases",
                column: "CaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CreatedById",
                table: "Cases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DeletedById",
                table: "Cases",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_DepartmentId",
                table: "Cases",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_Id",
                table: "Cases",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_IsDeleted",
                table: "Cases",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_LastModifiedById",
                table: "Cases",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTags_CaseId",
                table: "CaseTags",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseVehicles_VehicleId",
                table: "CaseVehicles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_BookingId",
                table: "Charges",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_Id",
                table: "Charges",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Charges_PersonId",
                table: "Charges",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Charges_UserId",
                table: "Charges",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeSceneCases_CrimeSceneId",
                table: "CrimeSceneCases",
                column: "CrimeSceneId");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeScenePersons_CrimeSceneId",
                table: "CrimeScenePersons",
                column: "CrimeSceneId");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeScenePersons_Id",
                table: "CrimeScenePersons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrimeScenePersons_PersonId",
                table: "CrimeScenePersons",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CrimeScenes_Id",
                table: "CrimeScenes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrimeScenes_LocationId",
                table: "CrimeScenes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Id",
                table: "Departments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_CaseId",
                table: "Evidences",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_CreatedById",
                table: "Evidences",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_DeletedById",
                table: "Evidences",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Evidences_Id",
                table: "Evidences",
                column: "Id",
                unique: true);

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
                name: "IX_Locations_Id",
                table: "Locations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DeletedById",
                table: "Persons",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FullName",
                table: "Persons",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Id",
                table: "Persons",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_IsDeleted",
                table: "Persons",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_Id",
                table: "Properties",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_LocationId",
                table: "Properties",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_BookingId",
                table: "Releases",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Releases_Id",
                table: "Releases",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Releases_PersonId",
                table: "Releases",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_UserId",
                table: "Releases",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CaseId",
                table: "Reports",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_CreatedById",
                table: "Reports",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_DeletedById",
                table: "Reports",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id",
                table: "Reports",
                column: "Id",
                unique: true);

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
                name: "IX_Tags_CreatedById",
                table: "Tags",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Id",
                table: "Tags",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LastModifiedById",
                table: "Tags",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Id",
                table: "Vehicles",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserCases");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CaseActions");

            migrationBuilder.DropTable(
                name: "CaseEdits");

            migrationBuilder.DropTable(
                name: "CaseNotes");

            migrationBuilder.DropTable(
                name: "CasePersons");

            migrationBuilder.DropTable(
                name: "CaseTags");

            migrationBuilder.DropTable(
                name: "CaseVehicles");

            migrationBuilder.DropTable(
                name: "Charges");

            migrationBuilder.DropTable(
                name: "CrimeSceneCases");

            migrationBuilder.DropTable(
                name: "CrimeScenePersons");

            migrationBuilder.DropTable(
                name: "Evidences");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Releases");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "CrimeScenes");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Cases");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
