using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTransakcjaToSingleProdukt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProduktTransakcja");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcja_id_produktu",
                table: "Transakcja",
                column: "id_produktu");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcja_Produkt_id_produktu",
                table: "Transakcja",
                column: "id_produktu",
                principalTable: "Produkt",
                principalColumn: "id_produktu",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transakcja_Produkt_id_produktu",
                table: "Transakcja");

            migrationBuilder.DropIndex(
                name: "IX_Transakcja_id_produktu",
                table: "Transakcja");

            migrationBuilder.CreateTable(
                name: "ProduktTransakcja",
                columns: table => new
                {
                    Produktyidproduktu = table.Column<int>(name: "Produktyid_produktu", type: "INTEGER", nullable: false),
                    Transakcjeidtransakcji = table.Column<int>(name: "Transakcjeid_transakcji", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduktTransakcja", x => new { x.Produktyidproduktu, x.Transakcjeidtransakcji });
                    table.ForeignKey(
                        name: "FK_ProduktTransakcja_Produkt_Produktyid_produktu",
                        column: x => x.Produktyidproduktu,
                        principalTable: "Produkt",
                        principalColumn: "id_produktu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduktTransakcja_Transakcja_Transakcjeid_transakcji",
                        column: x => x.Transakcjeidtransakcji,
                        principalTable: "Transakcja",
                        principalColumn: "id_transakcji",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProduktTransakcja_Transakcjeid_transakcji",
                table: "ProduktTransakcja",
                column: "Transakcjeid_transakcji");
        }
    }
}
