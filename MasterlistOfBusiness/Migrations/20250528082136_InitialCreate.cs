using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produkt",
                columns: table => new
                {
                    idproduktu = table.Column<int>(name: "id_produktu", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    typ = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    opis = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produkt", x => x.idproduktu);
                });

            migrationBuilder.CreateTable(
                name: "Uzytkownik",
                columns: table => new
                {
                    login = table.Column<string>(type: "TEXT", nullable: false),
                    haslo = table.Column<string>(type: "TEXT", nullable: false),
                    typ = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownik", x => x.login);
                });

            migrationBuilder.CreateTable(
                name: "Sprzedawca",
                columns: table => new
                {
                    idsprzedawcy = table.Column<int>(name: "id_sprzedawcy", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    login = table.Column<string>(type: "TEXT", nullable: false),
                    Uzytkowniklogin = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprzedawca", x => x.idsprzedawcy);
                    table.ForeignKey(
                        name: "FK_Sprzedawca_Uzytkownik_Uzytkowniklogin",
                        column: x => x.Uzytkowniklogin,
                        principalTable: "Uzytkownik",
                        principalColumn: "login");
                });

            migrationBuilder.CreateTable(
                name: "Konto",
                columns: table => new
                {
                    idkonta = table.Column<int>(name: "id_konta", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idsprzedawcy = table.Column<int>(name: "id_sprzedawcy", type: "INTEGER", nullable: true),
                    link = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    NazwaUzytkownika = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Platforma = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Sprzedawcaidsprzedawcy = table.Column<int>(name: "Sprzedawcaid_sprzedawcy", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konto", x => x.idkonta);
                    table.ForeignKey(
                        name: "FK_Konto_Sprzedawca_Sprzedawcaid_sprzedawcy",
                        column: x => x.Sprzedawcaidsprzedawcy,
                        principalTable: "Sprzedawca",
                        principalColumn: "id_sprzedawcy",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inwentarz",
                columns: table => new
                {
                    idinwentarza = table.Column<int>(name: "id_inwentarza", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idkonta = table.Column<int>(name: "id_konta", type: "INTEGER", nullable: false),
                    idproduktu = table.Column<int>(name: "id_produktu", type: "INTEGER", nullable: false),
                    cena = table.Column<int>(type: "INTEGER", nullable: false),
                    ilosc = table.Column<int>(type: "INTEGER", nullable: false),
                    Kontoidkonta = table.Column<int>(name: "Kontoid_konta", type: "INTEGER", nullable: false),
                    Produktidproduktu = table.Column<int>(name: "Produktid_produktu", type: "INTEGER", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Transakcja",
                columns: table => new
                {
                    idtransakcji = table.Column<int>(name: "id_transakcji", type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idkonta = table.Column<int>(name: "id_konta", type: "INTEGER", nullable: false),
                    idproduktu = table.Column<int>(name: "id_produktu", type: "INTEGER", nullable: false),
                    Inwentarzidinwentarza = table.Column<int>(name: "Inwentarzid_inwentarza", type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcja", x => x.idtransakcji);
                    table.ForeignKey(
                        name: "FK_Transakcja_Inwentarz_Inwentarzid_inwentarza",
                        column: x => x.Inwentarzidinwentarza,
                        principalTable: "Inwentarz",
                        principalColumn: "id_inwentarza");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inwentarz_Kontoid_konta",
                table: "Inwentarz",
                column: "Kontoid_konta");

            migrationBuilder.CreateIndex(
                name: "IX_Inwentarz_Produktid_produktu",
                table: "Inwentarz",
                column: "Produktid_produktu");

            migrationBuilder.CreateIndex(
                name: "IX_Konto_Sprzedawcaid_sprzedawcy",
                table: "Konto",
                column: "Sprzedawcaid_sprzedawcy");

            migrationBuilder.CreateIndex(
                name: "IX_Sprzedawca_Uzytkowniklogin",
                table: "Sprzedawca",
                column: "Uzytkowniklogin");

            migrationBuilder.CreateIndex(
                name: "IX_Transakcja_Inwentarzid_inwentarza",
                table: "Transakcja",
                column: "Inwentarzid_inwentarza");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transakcja");

            migrationBuilder.DropTable(
                name: "Inwentarz");

            migrationBuilder.DropTable(
                name: "Konto");

            migrationBuilder.DropTable(
                name: "Produkt");

            migrationBuilder.DropTable(
                name: "Sprzedawca");

            migrationBuilder.DropTable(
                name: "Uzytkownik");
        }
    }
}
