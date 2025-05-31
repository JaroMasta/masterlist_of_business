using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProduktModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sprzedawca_Uzytkownik_Uzytkowniklogin",
                table: "Sprzedawca");

            migrationBuilder.DropForeignKey(
                name: "FK_Transakcja_Inwentarz_Inwentarzid_inwentarza",
                table: "Transakcja");

            migrationBuilder.DropTable(
                name: "Inwentarz");

            migrationBuilder.DropIndex(
                name: "IX_Transakcja_Inwentarzid_inwentarza",
                table: "Transakcja");

            migrationBuilder.DropColumn(
                name: "Inwentarzid_inwentarza",
                table: "Transakcja");

            migrationBuilder.RenameColumn(
                name: "Uzytkowniklogin",
                table: "Sprzedawca",
                newName: "UzytkownikLogin");

            migrationBuilder.RenameIndex(
                name: "IX_Sprzedawca_Uzytkowniklogin",
                table: "Sprzedawca",
                newName: "IX_Sprzedawca_UzytkownikLogin");

            migrationBuilder.AlterColumn<string>(
                name: "HasloHash",
                table: "Uzytkownik",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "UzytkownikLogin",
                table: "Sprzedawca",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Kontoid_konta",
                table: "Produkt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "cena",
                table: "Produkt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "id_konta",
                table: "Produkt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ilosc",
                table: "Produkt",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_Produkt_Kontoid_konta",
                table: "Produkt",
                column: "Kontoid_konta");

            migrationBuilder.CreateIndex(
                name: "IX_ProduktTransakcja_Transakcjeid_transakcji",
                table: "ProduktTransakcja",
                column: "Transakcjeid_transakcji");

            migrationBuilder.AddForeignKey(
                name: "FK_Produkt_Konto_Kontoid_konta",
                table: "Produkt",
                column: "Kontoid_konta",
                principalTable: "Konto",
                principalColumn: "id_konta",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sprzedawca_Uzytkownik_UzytkownikLogin",
                table: "Sprzedawca",
                column: "UzytkownikLogin",
                principalTable: "Uzytkownik",
                principalColumn: "login",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produkt_Konto_Kontoid_konta",
                table: "Produkt");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprzedawca_Uzytkownik_UzytkownikLogin",
                table: "Sprzedawca");

            migrationBuilder.DropTable(
                name: "ProduktTransakcja");

            migrationBuilder.DropIndex(
                name: "IX_Produkt_Kontoid_konta",
                table: "Produkt");

            migrationBuilder.DropColumn(
                name: "Kontoid_konta",
                table: "Produkt");

            migrationBuilder.DropColumn(
                name: "cena",
                table: "Produkt");

            migrationBuilder.DropColumn(
                name: "id_konta",
                table: "Produkt");

            migrationBuilder.DropColumn(
                name: "ilosc",
                table: "Produkt");

            migrationBuilder.RenameColumn(
                name: "UzytkownikLogin",
                table: "Sprzedawca",
                newName: "Uzytkowniklogin");

            migrationBuilder.RenameIndex(
                name: "IX_Sprzedawca_UzytkownikLogin",
                table: "Sprzedawca",
                newName: "IX_Sprzedawca_Uzytkowniklogin");

            migrationBuilder.AlterColumn<string>(
                name: "HasloHash",
                table: "Uzytkownik",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Inwentarzid_inwentarza",
                table: "Transakcja",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uzytkowniklogin",
                table: "Sprzedawca",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "Inwentarz",
                columns: table => new
                {
                    idinwentarza = table.Column<int>(name: "id_inwentarza", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Kontoidkonta = table.Column<int>(name: "Kontoid_konta", type: "INTEGER", nullable: false),
                    Produktidproduktu = table.Column<int>(name: "Produktid_produktu", type: "INTEGER", nullable: false),
                    cena = table.Column<int>(type: "INTEGER", nullable: false),
                    idkonta = table.Column<int>(name: "id_konta", type: "INTEGER", nullable: false),
                    idproduktu = table.Column<int>(name: "id_produktu", type: "INTEGER", nullable: false),
                    ilosc = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inwentarz", x => x.idinwentarza);
                    table.ForeignKey(
                        name: "FK_Inwentarz_Konto_Kontoid_konta",
                        column: x => x.Kontoidkonta,
                        principalTable: "Konto",
                        principalColumn: "id_konta",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inwentarz_Produkt_Produktid_produktu",
                        column: x => x.Produktidproduktu,
                        principalTable: "Produkt",
                        principalColumn: "id_produktu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transakcja_Inwentarzid_inwentarza",
                table: "Transakcja",
                column: "Inwentarzid_inwentarza");

            migrationBuilder.CreateIndex(
                name: "IX_Inwentarz_Kontoid_konta",
                table: "Inwentarz",
                column: "Kontoid_konta");

            migrationBuilder.CreateIndex(
                name: "IX_Inwentarz_Produktid_produktu",
                table: "Inwentarz",
                column: "Produktid_produktu");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprzedawca_Uzytkownik_Uzytkowniklogin",
                table: "Sprzedawca",
                column: "Uzytkowniklogin",
                principalTable: "Uzytkownik",
                principalColumn: "login");

            migrationBuilder.AddForeignKey(
                name: "FK_Transakcja_Inwentarz_Inwentarzid_inwentarza",
                table: "Transakcja",
                column: "Inwentarzid_inwentarza",
                principalTable: "Inwentarz",
                principalColumn: "id_inwentarza");
        }
    }
}
