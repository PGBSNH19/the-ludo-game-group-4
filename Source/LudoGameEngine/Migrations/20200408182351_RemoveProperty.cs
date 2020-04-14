using Microsoft.EntityFrameworkCore.Migrations;

namespace LudoGameEngine.Migrations
{
    public partial class RemoveProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PieceFinished",
                table: "PlayerPiece");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PieceFinished",
                table: "PlayerPiece",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
