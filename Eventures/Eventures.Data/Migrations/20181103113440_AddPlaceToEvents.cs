namespace Eventures.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPlaceToEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Place",
                table: "Events");
        }
    }
}