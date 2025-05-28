using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    /// <inheritdoc />
    public partial class DodajHasloHashDoUzytkownik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "haslo",
                table: "Uzytkownik",
                newName: "HasloHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HasloHash",
                table: "Uzytkownik",
                newName: "haslo");
        }
    }
}
