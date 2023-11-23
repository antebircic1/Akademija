using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImeValute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KupovniTecaj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SrednjiTecaj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProdajniTecaj = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DatumUnosa = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExchangeRates");
        }
    }
}
