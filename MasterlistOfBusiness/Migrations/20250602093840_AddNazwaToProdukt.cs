using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MasterlistOfBusiness.Migrations
{
    /// <inheritdoc />
    public partial class AddNazwaToProdukt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "nazwa",
                table: "Produkt",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "nazwa",
                table: "Produkt");
        }
    }
}
