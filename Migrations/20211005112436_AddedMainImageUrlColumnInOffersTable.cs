using Microsoft.EntityFrameworkCore.Migrations;

namespace ActivitySimulator.Migrations
{
    public partial class AddedMainImageUrlColumnInOffersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImageUrl",
                table: "Offers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImageUrl",
                table: "Offers");
        }
    }
}
