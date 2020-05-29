using Microsoft.EntityFrameworkCore.Migrations;

namespace KOULIBALY.Migrations
{
    public partial class AddedBoolPlayedInPoule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlayed",
                table: "Poules",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlayed",
                table: "Poules");
        }
    }
}
