using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameEngine.Migrations
{
    public partial class AddColumnToPiece : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerPieceID",
                table: "Piece",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayerPieceID",
                table: "Piece");
        }
    }
}
