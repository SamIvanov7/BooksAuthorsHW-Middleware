using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "libdb");

            migrationBuilder.CreateTable(
                name: "Authors",
                schema: "libdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bocks",
                schema: "libdb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BocksAuthors",
                schema: "libdb",
                columns: table => new
                {
                    BockId = table.Column<Guid>(type: "uuid", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BocksAuthors", x => new { x.BockId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BocksAuthors_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalSchema: "libdb",
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BocksAuthors_Bocks_BockId",
                        column: x => x.BockId,
                        principalSchema: "libdb",
                        principalTable: "Bocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BocksAuthors_AuthorId",
                schema: "libdb",
                table: "BocksAuthors",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BocksAuthors",
                schema: "libdb");

            migrationBuilder.DropTable(
                name: "Authors",
                schema: "libdb");

            migrationBuilder.DropTable(
                name: "Bocks",
                schema: "libdb");
        }
    }
}
